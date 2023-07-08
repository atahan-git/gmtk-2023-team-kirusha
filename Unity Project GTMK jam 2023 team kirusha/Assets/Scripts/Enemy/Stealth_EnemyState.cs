using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth_EnemyState : TemplateEnemyState
{


    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        MoveToObject(gmManager.EnemyTarget);
    }

    private void MoveToObject(GameObject _obj)
    {
        rb.velocity = (_obj.transform.position
            - stateMachine.transform.position).normalized;
    }
}
