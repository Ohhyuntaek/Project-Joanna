using JoanStates;
using UnityEngine;

public class JoanToCrounch : State<Joan>
{
    public JoanToCrounch(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: Joan To Crounch State");
        user.ChangeAnimation("JoanToCrounch");
    }

    public override void Execute()
    {
        user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);

        AnimatorStateInfo stateInfo = user.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("JoanToCrounch") && stateInfo.normalizedTime >= 1)
        {
            user.ChangeAnimation("JoanCrounch");
            user.switchCrounchCollider(true);
        }

    }

    public override void Exit()
    {

    }

    public override void OnTransition()
    {
        if (Input.GetKeyUp(KeyCode.S)) 
        {
            user.ChangeState(JoanState.OutCrounch);
        }
    }
}

public class JoanOutCrounch : State<Joan>
{
    private bool isAnimationComplete = false;

    public JoanOutCrounch(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: Joan Out Crounch State");
        user.ChangeAnimation("JoanOutCrounch");
        isAnimationComplete = false;
    }

    public override void Execute()
    {
        user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);

        AnimatorStateInfo stateInfo = user.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("JoanOutCrounch") && stateInfo.normalizedTime >= 1)
        {
            isAnimationComplete = true;
            user.switchCrounchCollider(false);
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
