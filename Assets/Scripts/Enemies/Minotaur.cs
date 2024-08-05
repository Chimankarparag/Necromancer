using System.Collections;
using UnityEngine;

public class Minotaur : MonoBehaviour, IEnemy
{
    [SerializeField] private float attackDamage = 15f;
    [SerializeField] private Transform weaponCollider;

    private EnemyAI enemyAI;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private PlayerHealth playerHealth;
    private float attackRange = 3f;
    private float attackCooldown = 2f;
    private bool canAttack = true;
    private bool facingRight = true;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        circleCollider.enabled = false;
        weaponCollider.gameObject.SetActive(false);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(PlayerController.Instance.transform.position, transform.position);

        if (distanceToPlayer <= attackRange && canAttack)
        {
            Attack();
        }

        Flip();
    }

    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        Vector3 weaponColliderLocalPosition = weaponCollider.localPosition;

        if (PlayerController.Instance != null)
        {
            if (PlayerController.Instance.transform.position.x < transform.position.x && facingRight)
            {
                facingRight = false;
                // scale.x *= -1;
                weaponColliderLocalPosition.x *= -1;
            }
            else if (PlayerController.Instance.transform.position.x > transform.position.x && !facingRight)
            {
                facingRight = true;
                // scale.x *= -1;
                weaponColliderLocalPosition.x *= -1;
            }

            transform.localScale = scale;
            weaponCollider.localPosition = weaponColliderLocalPosition;
        }
    }

    public void Attack()
    {
        weaponCollider.gameObject.SetActive(true);
        int attackChoice = Random.Range(1, 4); // Random number between 1 and 3
        switch (attackChoice)
        {
            case 1:
                StartCoroutine(PerformAttack("Attack1"));
                break;
            case 2:
                StartCoroutine(PerformAttack("Attack2"));
                break;
            case 3:
                StartCoroutine(PerformAttack("Attack3"));
                break;
        }
    }

    private IEnumerator PerformAttack(string attackAnimation)
    {
        canAttack = false;
        animator.SetTrigger(attackAnimation);

        // Wait for the attack animation to finish
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimation) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        weaponCollider.gameObject.SetActive(false);

        // Set AfterAttack trigger
        animator.SetTrigger("AfterAttack");

        // Wait for the AfterAttack animation to finish
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("AfterAttack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        // Cooldown period
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage, transform);
            }
        }
    }
}
