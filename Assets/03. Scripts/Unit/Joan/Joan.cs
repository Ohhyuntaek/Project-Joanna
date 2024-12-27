using UnityEngine;
using JoanStates;

public class Joan : MonoBehaviour
{
    private JoanState prevJoanState = JoanState.Idle;
    private JoanState joanState = JoanState.Idle;

    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float castSize = 0;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private GameObject crounchCollider;

    public State<Joan>[] states;
    public Animator animator;

    [SerializeField] public float yVelocity = 0;
    [SerializeField] public float walkingValue = 2.5f;
    [SerializeField] public float runningValue = 4.0f;
    [SerializeField] public float jumpForce = 0;
    [SerializeField] public float landingSpeed = 0;
    [SerializeField] public bool isWalking = false;
    [SerializeField] public bool isRunning = false;
    [SerializeField] public bool isAttacking = false;
    [SerializeField] public bool isGround = false;

    public float moveSpeed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isAttacking = false;

        animator = GetComponentInChildren<Animator>();

        switchCrounchCollider(false);
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        ApplyGravity();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(JoanState.Jump);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            moveSpeed = runningValue;
        }
        else
        {
            isRunning = false;
            moveSpeed = walkingValue;
        }

        if (Input.GetKeyDown(KeyCode.S) && isGround)
        {
            ChangeState(JoanState.ToCrounch);
        }

        if (Input.GetMouseButtonDown(0) && isAttacking == false && isGround)
        {
            isAttacking = true;
            ChangeState(JoanState.LightAtk);
        }

        states[(int)joanState].Execute();
        states[(int)joanState].OnTransition();

        if (!isGround)
        {
            animator.SetFloat("YSpeed", yVelocity);
        }
    }

    private void OnEnable()
    {
        InitFSM();
    }

    private void InitFSM()
    {
        animator = GetComponentInChildren<Animator>();
        joanState = JoanState.Idle;

        states = new State<Joan>[(int)JoanState.Last];

        states[(int)JoanState.Idle]         = new JoanIdle(this);
        states[(int)JoanState.ToWalk]       = new JoanToWalk(this);
        states[(int)JoanState.Walking]      = new JoanWalking(this);
        states[(int)JoanState.BreakWalk]    = new JoanBreakWalk(this);
        states[(int)JoanState.ToRun]        = new JoanToRun(this);
        states[(int)JoanState.Running]      = new JoanRunning(this);
        states[(int)JoanState.BreakRun]     = new JoanBreakRun(this);
        states[(int)JoanState.TrickTurn]    = new JoanTrickTurn(this);
        states[(int)JoanState.Falling]      = new JoanFalling(this);
        states[(int)JoanState.Land]         = new JoanLand(this);
        states[(int)JoanState.Jump]         = new JoanJump(this);
        states[(int)JoanState.LandHard]     = new JoanLandHard(this);
        states[(int)JoanState.ToCrounch]    = new JoanToCrounch(this);
        states[(int)JoanState.OutCrounch]   = new JoanOutCrounch(this);
        states[(int)JoanState.LightAtk]     = new JoanLightAtk(this);
        states[(int)JoanState.UpLightAtk]   = new JoanUpLightAtk(this);

        states[(int)joanState].Enter();
    }

    public void ChangeAnimation(JoanAnimation newAnimation, float normalizedTime = 0)
    {
        animator.Play(newAnimation.ToString(), 0, normalizedTime);
    }

    public void ChangeAnimation(string animationName, float normalizedTime = 0)
    {
        animator.Play(animationName, 0, normalizedTime);
    }

    public void ChangeState(JoanState state)
    {
        states[(int)joanState].Exit();
        prevJoanState = joanState;
        joanState = state;
        states[(int)joanState].Enter();
    }

    private void GroundCheck()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        if (yVelocity <= 0)
        {
            Debug.DrawLine(rigidbody.position + Vector2.up, rigidbody.position + Vector2.up + (Vector2.down * castSize), Color.red);

            RaycastHit2D rayHit = Physics2D.Raycast(rigidbody.position + Vector2.up, Vector2.down, castSize, floorLayer);
            if (rayHit.collider != null)
            {
                if (!isGround)
                {
                    transform.position = rayHit.point;
                }
                isGround = true;
                yVelocity = 0;
            }
            else
            {
                isGround = false;
                ChangeState(JoanState.Falling);
            }
        }
    }

    public void ApplyGravity()
    {
        if (!isGround)
        {
            yVelocity -= gravity * gravity * Time.deltaTime;
        }
        Vector3 position = transform.position;
     
        position.y += yVelocity * Time.deltaTime;
        transform.position = position;
    }

    public void switchCrounchCollider(bool trigger)
    {
        Collider2D col = GetComponent<Collider2D>();
        if (trigger)
        {
            col.enabled = false;    
            crounchCollider.SetActive(true);
        }
        else
        {
            col.enabled = true;
            crounchCollider.SetActive(false);
        }
    }
}
