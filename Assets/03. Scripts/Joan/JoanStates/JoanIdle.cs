using UnityEngine;
using JoanStates;

public class Idle : State<Joan>
{
    public Idle(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: Idle State");
        user.ChangeAnimation("JoanIdle");
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void OnTransition()
    {
        
    }
}



