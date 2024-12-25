using JoanStates;
using UnityEngine;


public class JoanLand : State<Joan>
{
    private bool isAnimationComplete = false;

    public JoanLand(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: Joan Land State");
        user.ChangeAnimation("JoanLand");
        isAnimationComplete = false;
    }

    public override void Execute()
    {
        user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);

        AnimatorStateInfo stateInfo = user.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("JoanLand") && stateInfo.normalizedTime >= 1)
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
