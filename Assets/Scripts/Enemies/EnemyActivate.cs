using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivate : MonoBehaviour
{
    [SerializeField] private float activationDistance = 8.5f; 
    [SerializeField] private Transform enemyTransform; 
    [SerializeField] private bool isSummoner;

    public bool isActivated = false;
    private Rigidbody2D enemyRigidbody;
    private EnemyAI enemyAI;
    private Transform playerTransform;
    private EnemyHealth enemyHealth;

    private Summoner summoner;



    private void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>();
        enemyHealth = GetComponent<EnemyHealth>();
        summoner = GetComponent<Summoner>();

        playerTransform = PlayerController.Instance.transform;


        FreezeEnemy();
    }

    private void Update()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector2.Distance(enemyTransform.position, playerTransform.position);

        if (!isActivated && distanceToPlayer <= activationDistance)
        {
            ActivateEnemy();
        }
    }

    public void ActivateEnemy()
    {
        isActivated = true;
        UnfreezeEnemy();
        if(isSummoner){
            summoner.StartSpawn();
        }
       
    }

    public void FreezeEnemy()
    {
        if (enemyRigidbody != null)
        {
            enemyRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (enemyAI != null)
        {
            enemyAI.enabled = false;
        }
    }

    public void UnfreezeEnemy()
    {
        if (enemyRigidbody != null && enemyHealth.isDead==false) 
        {
            enemyRigidbody.constraints = RigidbodyConstraints2D.None;
            enemyRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (enemyAI != null)
        {
            enemyAI.enabled = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }
}
