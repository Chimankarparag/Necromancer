using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObstacle : Singleton<EnemyObstacle>
{
    [SerializeField] public float obstacleDamage = 5f;

    [SerializeField] public bool Poisonous = true;
    [SerializeField] public int poisonDamage =10;
    [SerializeField] public float poisonInterval = 2f;
    [SerializeField] public float poisonDuration = 10f;

    
}


