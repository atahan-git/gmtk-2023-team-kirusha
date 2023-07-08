using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Components
    private StateMachine enemyStMachine;
  
    #endregion

    private void Awake()
    {   
        enemyStMachine = GetComponent<StateMachine>();
    }
    void Start()
    {
        enemyStMachine.SetNextState(new Idle_EnemyState());
        //enemyStMachine.SetNextState(new Stealth_EnemyState());
    }


    void Update()
    {
        
    }
}
