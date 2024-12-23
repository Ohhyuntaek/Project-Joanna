using UnityEngine;

public abstract class State<T> where T : MonoBehaviour
{
    public T user;

    public State(T user)
    {
        this.user = user;
    }

    public virtual void Enter() { }
    public abstract void Execute();
    public abstract void Exit();
    public abstract void OnTransition();

}
