using UnityEngine;

public class BossActivatorThrone : MonoBehaviour
{
    [SerializeField] private EnemyActivate enemyToActivate;

    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (enemyToActivate != null)
            {
                enemyToActivate.ActivateEnemy();
            }
        }
    }
}
