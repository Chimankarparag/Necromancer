using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{   
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    [SerializeField] public float attackByEnemy;


    public bool canAttack = true;
    
    private enum State {
        Roaming, 
        Attacking
    }

    private Vector2 roamPosition;
    private float timeRoaming = 0f;
    private State state;
    private EnemyPathfinding enemyPathfinding;
    private Animator animator;
    private EnemyHealth enemyHealth;
    private Transform playerTransform;
    private Rigidbody2D rb;

    private static readonly int GoRun = Animator.StringToHash("GoRun");
    private static readonly int GoIdle = Animator.StringToHash("GoIdle");

    public float approachDistance = 10f; // Distance at which the enemy will start approaching the player
    public float runAwayDistance = 5f; // Distance at which the enemy will start running away from the player
    public float moveSpeed = 2f; // Speed at which the enemy moves
    public float runAwaySpeed = 4f; // Speed at which the enemy runs away

    private void Awake() {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        animator = GetComponent<Animator>();
        state = State.Roaming;
    }

    private void Start() {
        roamPosition = GetRoamingPosition();
        enemyHealth = GetComponent<EnemyHealth>();
        rb = GetComponent<Rigidbody2D>();
        playerTransform = PlayerController.Instance.transform;
    }

    private void Update() {
        MovementStateControl();
        if(enemyHealth.isBossMonster){
            RangedEnemyMove();
        }
    }

    private void MovementStateControl() {
        switch (state)
        {
            default:
            case State.Roaming:
                Roaming();
            break;

            case State.Attacking:
                Attacking();
            break;
        }
    }

    private void Roaming() {
        timeRoaming += Time.deltaTime;

        if(enemyHealth.isSpecialMonster && !enemyHealth.isBossMonster) {
            // StartCoroutine(RoamingBehaviorGoblin());
            RangedEnemyMove();
        }
        else if(enemyHealth.isBossMonster){

            RangedEnemyMove();

        }else {
            enemyPathfinding.MoveTo(roamPosition);
        }
        if(PlayerController.Instance){


            if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange) {
                state = State.Attacking;
                StopAllCoroutines();
            }

            if ((timeRoaming > roamChangeDirFloat)) {
                roamPosition = GetRoamingPosition();
            }
        }
    }
    private IEnumerator RoamingBehaviorGoblin()
    {
        while (state == State.Roaming)
        {
            // GoRun animation and movement
            animator.ResetTrigger(GoIdle);
            animator.SetTrigger(GoRun);
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(3f);

            // GoIdle animation and stop
            animator.ResetTrigger(GoRun);
            animator.SetTrigger(GoIdle);
            enemyPathfinding.StopMoving();
            yield return new WaitForSeconds(2f);

        }
    }

    // private void ChangeFacingDirection()
    // {
    //     // Flip the sprite's facing direction
    //     var spriteRenderer = GetComponent<SpriteRenderer>();
    //     spriteRenderer.flipX = !spriteRenderer.flipX;
    // }

    private void Attacking() {
        if(PlayerController.Instance){
            
        
            if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
            {
                state = State.Roaming;
            }

            if (attackRange != 0 && canAttack) {


                canAttack = false;
                if(enemyHealth.isBossMonster) canAttack = true;
 
                StartCoroutine(AttackCooldownRoutine());

                (enemyType as IEnemy).Attack();

                if (stopMovingWhileAttacking) {
                    enemyPathfinding.StopMoving();

                } else if (!enemyHealth.isSpecialMonster){

                    enemyPathfinding.MoveTo(roamPosition);
                }
                if ( enemyHealth.isBossMonster){

                    RangedEnemyMove();

                }

                
            }
        }
    }

    private IEnumerator AttackCooldownRoutine() {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition() {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }





    public void RangedEnemyMove(){

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > approachDistance)
        {
            animator.ResetTrigger(GoIdle);
            animator.SetTrigger(GoRun);
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            enemyPathfinding.MoveTo(direction);
        }
        else if (distanceToPlayer < runAwayDistance)
        {
            animator.ResetTrigger(GoIdle);
            animator.SetTrigger(GoRun);
            Vector2 direction = (transform.position - playerTransform.position).normalized;
            enemyPathfinding.MoveTo(direction);
        }
        else
        {
            Vector2 direction = Vector2.zero;
            animator.ResetTrigger(GoRun);
            animator.SetTrigger(GoIdle);
            enemyPathfinding.MoveTo(direction);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, approachDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, runAwayDistance);
    }
    
}
