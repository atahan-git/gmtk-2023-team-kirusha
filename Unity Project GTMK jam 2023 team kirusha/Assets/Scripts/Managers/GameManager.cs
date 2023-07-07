using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    { get { return instance; } }

    [SerializeField]
    private GameObject enemyTarget;
    public GameObject EnemyTarget
    { get { return enemyTarget; } }

    private void Awake()
    {
        instance = this;    
    }

}
