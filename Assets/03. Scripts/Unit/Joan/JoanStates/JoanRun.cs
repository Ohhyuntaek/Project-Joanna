using JoanStates;
using UnityEngine;

public class JoanToRun : State<Joan>
{
    private bool isAnimationComplete = false;

    public JoanToRun(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: To Run State");

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3 scale = user.transform.localScale;
            scale.x = Input.GetAxisRaw("Horizontal");
            user.transform.localScale = scale;
        }

        user.ChangeAnimation("JoanToRun");
        isAnimationComplete = false;
    }

    public override void Execute()
    {
        user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);

        AnimatorStateInfo stateInfo = user.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("JoanToRun") && stateInfo.normalizedTime >= 1)
        {
            isAnimationComplete = true;
        }
    }

    public override void Exit()
    {
        
    }

    public override void OnTransition()
    {
        if (isAnimationComplete)
        {
            if (user.isRunning)
            {
                user.ChangeState(JoanState.Running);
            }
            else if (user.isWalking)
            {
                user.ChangeState(JoanState.Walking);
            }
        }
    }
}

public class JoanRunning : State<Joan>
{
    public JoanRunning(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: Joan Running State");
        user.moveSpeed = user.runningValue;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3 scale = user.transform.localScale;
            scale.x = Input.GetAxisRaw("Horizontal");
            user.transform.localScale = scale;
        }

        user.ChangeAnimation("JoanRunning");
    }

    public override void Execute()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3 scale = user.transform.localScale;
            scale.x = Input.GetAxisRaw("Horizontal");
            user.transform.localScale = scale;
        }

        user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(user.moveSpeed * Input.GetAxisRaw("Horizontal"), user.GetComponent<Rigidbody2D>().linearVelocity.y);
    }

    public override void Exit()
    {

    }

    public override void OnTransition()
    {
        if (!user.isRunning)
        {
            user.isRunning = false;
            user.ChangeState(JoanState.RunToWalk);
        } 
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            user.ChangeState(JoanState.BreakRun);
        }
    }
}

public class JoanBreakRun : State<Joan>
{
    private bool isAnimationComplete = false;

    public JoanBreakRun(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: Joan Break Run State");
        user.ChangeAnimation("JoanBreakRun");
        isAnimationComplete = false;
    }

    public override void Execute()
    {
        if (Input.GetAxisRaw("Horizontal") - user.transform.localScale.x == 0)
        {
            user.ChangeState(JoanState.ToRun);
        }
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal") - user.transform.localScale.x) > 1)
        {
            user.ChangeState(JoanState.TrickTurn);
        }

        user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);

        AnimatorStateInfo stateInfo = user.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("JoanBreakRun") && stateInfo.normalizedTime >= 1)
        {
            isAnimationComplete = true;
        }

    }

    public override void Exit()
    {

    }

    public override void OnTransition()
    {
        if (isAnimationComplete)
        {
            user.isRunning = false;
            user.ChangeState(JoanState.Idle);
        }
    }
}

public class JoanRunToWalk : State<Joan>
{
    private bool isAnimationComplete = false;

    public JoanRunToWalk(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: Joan Run to Walk State");
        user.ChangeAnimation("JoanRunToWalk");
        isAnimationComplete = false;
    }

    public override void Execute()
    {
        user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);

        AnimatorStateInfo stateInfo = user.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("JoanRunToWalk") && stateInfo.normalizedTime >= 1)
        {
            isAnimationComplete = true;
        }
    }

    public override void Exit() 
    {
        
    }

    public override void OnTransition()
    {
        if (isAnimationComplete)
        {
            user.ChangeState(JoanState.Walking);
        }
    }
}