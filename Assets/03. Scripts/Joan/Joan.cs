using UnityEngine;
using JoanStates;

public class Joan : MonoBehaviour
{
    public JoanState joanState = JoanState.Idle;
    public State<Joan>[] states;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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

        states[(int)JoanState.Idle] = new Idle(this);

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
}
