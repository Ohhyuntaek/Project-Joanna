using UnityEngine;
using JoanStates;

public class JoanIdle : State<Joan>
{
    public JoanIdle(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: Idle State");
        user.ChangeAnimation("JoanIdle");
        user.IdleInitalize();
    }

    public override void Execute()
    {
        user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
    }

    public override void Exit()
    {
        
    }

    public override void OnTransition()
    {
        if (!user.isGround)
        {
            user.ChangeState(JoanState.Falling);
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (user.isRunning)
                {
                    user.ChangeState(JoanState.ToRun);
                }
                else
                {
                    user.isWalking = true;
                    user.ChangeState(JoanState.ToWalk);
                }
            }

        }
    }
}



