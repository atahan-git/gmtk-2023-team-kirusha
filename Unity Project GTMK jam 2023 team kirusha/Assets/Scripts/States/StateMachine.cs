using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState { get; private set; }
    private State nextState;

    private void Update()
    {
        if (nextState != null)
        {
            SetState(nextState);
            nextState = null;
        }

        if(currentState != null) 
        {
             currentState.OnUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.OnFixedUpdate();
        }
    }

    public void SetNextState(State _nextState)
    {
        if (_nextState != null)
        {
            nextState = _nextState; 
        }
    }

    private void SetState(State _newState)
    {
        if(currentState != null)
        {
            currentState.OnExit();
        }
        currentState = _newState;
        currentState.OnEnter(this);
    }
}
