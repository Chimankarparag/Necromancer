using System.Collections;
using UnityEngine;

public class Goblin : MonoBehaviour, IEnemy
{
    private Animator animator;
    private EnemyAI enemyAI;
    private EnemyPathfinding enemyPathfinding;
    private bool isAttacking;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void Start()
    {
        StartCoroutine(RoamingBehavior());
    }

    private IEnumerator RoamingBehavior()
    {
        while (true)
        {
            if (!isAttacking)
            {
                // Roaming state behavior
                animator.SetTrigger("GoIdle");
                enemyPathfinding.StopMoving();
                yield return new WaitForSeconds(2f);

                animator.SetTrigger("GoRun");
                enemyPathfinding.MoveTo(GetRoamingPosition());
                yield return new WaitForSeconds(2f);
            }
            else
            {
                // Attacking state behavior
                animator.SetTrigger("GoIdle");
                enemyPathfinding.StopMoving();
                yield return new WaitForSeconds(1f);

                animator.SetTrigger("GoRun");
                enemyPathfinding.MoveTo(PlayerController.Instance.transform.position);
                yield return new WaitForSeconds(3f);
            }
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(1f, 5f);
    }

    public void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
    }
}
