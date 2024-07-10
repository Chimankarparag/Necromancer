using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivate : MonoBehaviour
{
    [SerializeField] private float activationDistance = 8.5f; 
    [SerializeField] private Transform enemyTransform; 

    private bool isActivated = false;
    private Rigidbody2D enemyRigidbody;
    private EnemyAI enemyAI;
    private Transform playerTransform;
    private EnemyHealth enemyHealth;



    private void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>();
        enemyHealth = GetComponent<EnemyHealth>();

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

    private void ActivateEnemy()
    {
        isActivated = true;
        UnfreezeEnemy();
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

    private void UnfreezeEnemy()
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
