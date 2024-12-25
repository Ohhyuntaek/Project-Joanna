using JoanStates;
using UnityEngine;

public class JoanTrickTurn : State<Joan>
{
    private bool isAnimationComplete = false;

    public JoanTrickTurn(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: Joan Trick Turn State");
        user.ChangeAnimation("JoanTrickTurn");
        isAnimationComplete = false;
    }

    public override void Execute()
    {
        user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);

        AnimatorStateInfo stateInfo = user.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("JoanTrickTurn") && stateInfo.normalizedTime >= 1)
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
            user.ChangeState(JoanState.Running);
        }
    }
}
