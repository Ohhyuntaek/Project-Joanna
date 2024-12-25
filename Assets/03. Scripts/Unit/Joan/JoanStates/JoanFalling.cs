using JoanStates;
using UnityEngine;

public class JoanFalling : State<Joan>
{
    public JoanFalling(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: Joan Falling State");
        user.ChangeAnimation("JoanFalling");
    }

    public override void Execute()
    {
        
    }

    public override void Exit() 
    {
    
    }

    public override void OnTransition()
    {
        if (user.isGround)
        {
            user.ChangeState(JoanState.Land);
        }
    }
}
