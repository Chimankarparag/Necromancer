using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{   
    [SerializeField] private int startingHealth=3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust= 15f;

    [SerializeField] public bool isSpecialMonster = false;
    [SerializeField] public bool isBossMonster = false;

    readonly int DEATH_HASH = Animator.StringToHash("Death");
    private int currentHealth;
    private EnemyAI enemyAI;
    private Knockback knockback;
    private Flash flash;
    private Animator myanimator;
    public bool isDead =false;
    private EnemyActivate enemyActivate;

    public event Action OnDeath;
   

    private void Awake(){
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        myanimator = GetComponent<Animator>();
        enemyActivate = GetComponent<EnemyActivate>();
    }

    private void Start(){
        currentHealth = startingHealth;
    }
    
    public void TakeDamage( int damage){
        currentHealth-= damage;
        Debug.Log("Enemy Health: " + currentHealth);
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());

    }

    private IEnumerator CheckDetectDeathRoutine(){
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }
    public void DetectDeath()
        {
            if (currentHealth <= 0)
            {
                
                if(!isDead) GetComponent<PickUpSpawnner>().DropItems(); 
                isDead =true;
                enemyActivate.FreezeEnemy();
                OnDeath?.Invoke();

                // GetComponent<PickUpSpawnner>().DropItems(); 
                if (isSpecialMonster ==true)
                {
                    // Debug.Log("special moster killed");
                    GetComponent<Animator>().SetTrigger(DEATH_HASH);
                    StartCoroutine(WaitForDeathAnimation());
                }
                else
                {
                    DestroyEnemy();
                }
            }
        }

        private IEnumerator WaitForDeathAnimation()
        {
            yield return new WaitForSeconds(myanimator.GetCurrentAnimatorStateInfo(0).length);
            // can add some time after death animation after ...length +
            Destroy(gameObject);
        }

        private void DestroyEnemy()
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }



