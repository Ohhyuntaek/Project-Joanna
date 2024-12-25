using JoanStates;
using UnityEngine;

public class JoanJump : State<Joan>
{
    private bool hasJumped = false;

    public JoanJump(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: Joan Jump State");
        user.ChangeAnimation("JoanJump");
        hasJumped = false;
    }

    public override void Execute()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3 scale = user.transform.localScale;
            scale.x = Input.GetAxisRaw("Horizontal");
            user.transform.localScale = scale;

            user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(user.moveSpeed * Input.GetAxisRaw("Horizontal"), user.GetComponent<Rigidbody2D>().linearVelocity.y);
        }

        if (!hasJumped && user.isGround)
        {
            user.yVelocity = user.jumpForce;
            user.isGround = false;
            hasJumped = true;
        }
    }

    public override void Exit()
    {
        
    }

    public override void OnTransition()
    {
        if (user.isGround)
        {
            user.ChangeState(JoanState.Land);
            hasJumped = false;
        }
    }
}
