using System.Collections;
using UnityEngine;

public class Tackle : MonoBehaviour, IEnemy
{
    private EnemyAI enemyAI;
    private float tackleDuration = 2f;
    private float roamDuration = 5f;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    public void Attack()
    {
        StartCoroutine(TackleRoutine());
    }

    private IEnumerator TackleRoutine()
    {
        float timer = 0f;
        Transform playerTransform = PlayerController.Instance.transform;

        // Move towards player for 2 seconds
        while (timer < tackleDuration)
        {
            timer += Time.deltaTime;
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            enemyAI.GetComponent<EnemyPathfinding>().MoveTo(direction);
            yield return null;
        }

        // Stop moving and enter roaming state for 3 seconds
        enemyAI.GetComponent<EnemyPathfinding>().StopMoving();
        enemyAI.StartCoroutine(RoamAfterAttack());
    }

    private IEnumerator RoamAfterAttack()
    {
        enemyAI.canAttack = false;
        yield return new WaitForSeconds(roamDuration);
        enemyAI.canAttack = true;

    }
}
