using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateEnemyState : State
{
    protected Rigidbody2D rb;
    protected GameManager gmManager;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        gmManager = GameManager.Instance;

        rb = stateMachine.GetComponent<Rigidbody2D>();

        Debug.Log("Enemy in " + stateMachine.currentState);
    }

}
