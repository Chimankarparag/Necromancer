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
    private EnemyHealth enemyHealth;


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

    private static readonly int GoRun = Animator.StringToHash("GoRun");
    private static readonly int GoIdle = Animator.StringToHash("GoIdle");

    private void Awake() {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        animator = GetComponent<Animator>();
        state = State.Roaming;
    }

    private void Start() {
        roamPosition = GetRoamingPosition();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Update() {
        MovementStateControl();
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

        if(enemyHealth.isSpecialMonster){
            StartCoroutine(RoamingBehavior());
        }
        else{
            enemyPathfinding.MoveTo(roamPosition);
        }
        if(PlayerController.Instance){


            if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange) {
                state = State.Attacking;
                 StopAllCoroutines();
            }

            if (timeRoaming > roamChangeDirFloat) {
                roamPosition = GetRoamingPosition();
            }
        }
    }
    private IEnumerator RoamingBehavior()
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

    private void ChangeFacingDirection()
    {
        // Flip the sprite's facing direction
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private void Attacking() {
        if(PlayerController.Instance){
            
        
            if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
            {
                state = State.Roaming;
            }

            if (attackRange != 0 && canAttack) {

                canAttack = false;
                (enemyType as IEnemy).Attack();

                if (stopMovingWhileAttacking) {
                    enemyPathfinding.StopMoving();
                } else {
                    enemyPathfinding.MoveTo(roamPosition);
                }

                StartCoroutine(AttackCooldownRoutine());
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

    
}
