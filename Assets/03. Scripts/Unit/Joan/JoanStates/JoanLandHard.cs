using JoanStates;
using UnityEngine;

public class JoanLandHard : State<Joan>
{
    private bool isAnimationComplete = false;

    public JoanLandHard(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: Joan Land State");
        user.ChangeAnimation("JoanLandHard");
        isAnimationComplete = false;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3 scale = user.transform.localScale;
            scale.x = Input.GetAxisRaw("Horizontal");
            user.transform.localScale = scale;

            user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(user.landingSpeed * Input.GetAxisRaw("Horizontal"), user.GetComponent<Rigidbody2D>().linearVelocity.y);
        }
    }

    public override void Execute()
    {
        AnimatorStateInfo stateInfo = user.animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("JoanLandHard") && stateInfo.normalizedTime > 0.7f && stateInfo.normalizedTime < 1)
        {
            user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        }
        else if (stateInfo.IsName("JoanLandHard") && stateInfo.normalizedTime >= 1)
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
            if (Input.GetAxisRaw("Horizontal") != 0)
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
            else
            {
                user.ChangeState(JoanState.Idle);
            }
        }
    }
}
