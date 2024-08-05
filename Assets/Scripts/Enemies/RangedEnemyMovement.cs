using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyMovement : MonoBehaviour
{

    public float approachDistance = 10f; // Distance at which the enemy will start approaching the player
    public float runAwayDistance = 5f; // Distance at which the enemy will start running away from the player
    public float moveSpeed = 2f; // Speed at which the enemy moves
    public float runAwaySpeed = 4f; // Speed at which the enemy runs away

    private Rigidbody2D rb;
    private Animator animator;
    private Transform playerTransform;
    private EnemyPathfinding enemyPathfinding;

    private static readonly int GoRun = Animator.StringToHash("GoRun");
    private static readonly int GoIdle = Animator.StringToHash("GoIdle");

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        if (playerTransform == null)
        {
            playerTransform = PlayerController.Instance.transform; 
        }
    }

    private void Update()
    {
        // if (playerTransform == null) return;

        // float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // if (distanceToPlayer > approachDistance)
        // {
        //     MoveTowardsPlayer();
        // }
        // else if (distanceToPlayer < runAwayDistance)
        // {
        //     RunAwayFromPlayer();
        // }
        // else
        // {
        //     StopMovement();
        // }
    }

    public void RangedEnemyMove(){

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > approachDistance)
        {
            MoveTowardsPlayer();
        }
        else if (distanceToPlayer < runAwayDistance)
        {
            RunAwayFromPlayer();
        }
        else
        {
            StopMovement();
        }
    }

    public void MoveTowardsPlayer()
    {
        // animator.ResetTrigger(GoIdle);
        // animator.SetTrigger(GoRun);
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * (moveSpeed * Time.fixedDeltaTime));

    }

    public void RunAwayFromPlayer()
    {
    //     animator.ResetTrigger(GoIdle);
    //     animator.SetTrigger(GoRun);
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        rb.MovePosition(rb.position + direction * (runAwaySpeed * Time.fixedDeltaTime));
    }

    public void StopMovement()
    {
        Vector2 direction = Vector3.zero;
        // animator.ResetTrigger(GoRun);
        // animator.SetTrigger(GoIdle);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, approachDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, runAwayDistance);
    }
}
