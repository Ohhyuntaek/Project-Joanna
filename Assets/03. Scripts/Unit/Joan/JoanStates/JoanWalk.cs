using JoanStates;
using UnityEngine;

public class JoanToWalk : State<Joan>
{
    private bool isAnimationComplete = false;

    public JoanToWalk(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: To Walk State");

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3 scale = user.transform.localScale;
            scale.x = Input.GetAxisRaw("Horizontal");
            user.transform.localScale = scale;
        }

        user.ChangeAnimation("JoanToWalk");
        isAnimationComplete = false;
    }

    public override void Execute()
    {
        user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);

        AnimatorStateInfo stateInfo = user.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("JoanToWalk") && stateInfo.normalizedTime >= 1)
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

public class JoanWalking : State<Joan>
{
    public JoanWalking(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: Walking State");
        user.ChangeAnimation("JoanWalking");
    }

    public override void Execute()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3 scale = user.transform.localScale;
            scale.x = Input.GetAxisRaw("Horizontal");
            user.transform.localScale = scale;
        }

        user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(user.walkSpeed * Input.GetAxisRaw("Horizontal"), user.GetComponent<Rigidbody2D>().linearVelocity.y);
    }

    public override void Exit() 
    { 
        
    }

    public override void OnTransition()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            user.ChangeState(JoanState.BreakWalk);
        }
    }
}

public class JoanBreakWalk : State<Joan>
{
    private bool isAnimationComplete = false;

    public JoanBreakWalk(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: Break Walk State");
        user.ChangeAnimation("JoanBreakWalk");
        isAnimationComplete = false;
    }

    public override void Execute()
    {
        user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);

        AnimatorStateInfo stateInfo = user.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("JoanBreakWalk") && stateInfo.normalizedTime >= 1)
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
            user.ChangeState(JoanState.Idle);
        }
    }
}
