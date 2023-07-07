using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class State 
{
    protected float fixedTime { get; set; }

    public StateMachine stateMachine;

    public virtual void OnEnter(StateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public virtual void OnUpdate()
    {
        
    }

    public virtual void OnFixedUpdate()
    {
        fixedTime += Time.deltaTime;
    }

    public virtual void OnExit()
    {

    }

    #region Passthrough Methods
    protected static void Destroy(UnityEngine.Object obj)
    {
        UnityEngine.Object.Destroy(obj);
    }
    protected T GetComponent<T>() where T : Component { return stateMachine.GetComponent<T>(); }

    protected Component GetComponent(System.Type type) { return stateMachine.GetComponent(type); }

    protected Component GetComponent(string type) { return stateMachine.GetComponent(type); }
    #endregion

}
