using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTNode<T> where T : class
{
    public T Blackboard;
    public BTNode(T _blackboard)
    {
        Blackboard = _blackboard;
    }

    public enum States
    {
        Done,
        Running,
        Fail,
        None
    }

    protected States currentState = States.None;
    protected States lastState = States.None;

    public abstract bool CanHaveChilds();
    protected abstract States Run();
    protected abstract void Reset();
    protected abstract void Awake();
    protected abstract void Sleep();

    public States Update()
    {
        if (currentState == States.None)
        {
            Awake();
        }

        currentState = Run();
        lastState = currentState;

        if (currentState != States.Running)
        {
            Sleep();
            Reset();
            currentState = States.None;
        }

        return lastState;
    }
}
