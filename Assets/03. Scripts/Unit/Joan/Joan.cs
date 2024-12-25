using UnityEngine;
using JoanStates;

public class Joan : MonoBehaviour
{
    private JoanState prevJoanState = JoanState.Idle;
    private JoanState joanState = JoanState.Idle;

    [SerializeField] private float yVelocity = 0;
    [SerializeField] float gravity = 9.8f;
    [SerializeField] private float castSize = 0;
    [SerializeField] private LayerMask floorLayer;

    public State<Joan>[] states;
    public Animator animator;

    [SerializeField]
    public float walkingFloat = 2.5f;
    [SerializeField]
    public float runningFloat = 4.0f;
    [SerializeField]
    public bool isWalking = false;
    [SerializeField]
    public bool isRunning = false;
    [SerializeField]
    public bool isGround = false;

    public float moveSpeed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        ApplyGravity();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            moveSpeed = runningFloat;
        }
        else
        {
            isRunning = false;
            moveSpeed = walkingFloat;
        }

        states[(int)joanState].Execute();
        states[(int)joanState].OnTransition();
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

        states[(int)JoanState.Idle] = new JoanIdle(this);
        states[(int)JoanState.ToWalk] = new JoanToWalk(this);
        states[(int)JoanState.Walking] = new JoanWalking(this);
        states[(int)JoanState.BreakWalk] = new JoanBreakWalk(this);
        states[(int)JoanState.ToRun] = new JoanToRun(this);
        states[(int)JoanState.Running] = new JoanRunning(this);
        states[(int)JoanState.BreakRun] = new JoanBreakRun(this);
        states[(int)JoanState.TrickTurn] = new JoanTrickTurn(this);

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
}
