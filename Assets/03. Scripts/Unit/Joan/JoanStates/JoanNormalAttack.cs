using JoanStates;
using UnityEditor;
using UnityEngine;

public class JoanLightAtk : State<Joan>
{
    private bool isAnimationComplete = false;
    private bool isChangeAttack = false;

    public JoanLightAtk(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: Joan Light Atk State");
        user.ChangeAnimation("JoanLightAtk");
        isAnimationComplete = false;
        isChangeAttack = false;
    }

    public override void Execute()
    {
        user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);

        AnimatorStateInfo stateInfo = user.animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("JoanLightAtk") && stateInfo.normalizedTime > 0.3 && stateInfo.normalizedTime < 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isChangeAttack = true;
            }
        }
        else if (stateInfo.IsName("JoanLightAtk") && stateInfo.normalizedTime >= 1)
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
            if (isChangeAttack)
            {
                user.ChangeState(JoanState.UpLightAtk);
            }
            else
            {
                user.isAttacking = false;
                user.ChangeState(JoanState.Idle);
            }
        }
    }
}

public class JoanUpLightAtk : State<Joan>
{
    private bool isAnimationComplete = false;

    public JoanUpLightAtk(Joan user) : base(user) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Joan: Joan Up Light Atk State");
        user.ChangeAnimation("JoanUpLightAtk");
        isAnimationComplete = false;
    }

    public override void Execute()
    {
        user.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);

        AnimatorStateInfo stateInfo = user.animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("JoanUpLightAtk") && stateInfo.normalizedTime >= 1)
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
            user.isAttacking = false;
            user.ChangeState(JoanState.Idle);
        }
    }
}
