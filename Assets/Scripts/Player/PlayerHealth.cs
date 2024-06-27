using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead{ get; private set; }
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] public GameObject DontDestroy;

    

    public bool isPoisoned = false;

    private float currentHealth;
    private Slider healthSlider;
    private TextMeshProUGUI healthText;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;
    const string HEALTH_SLIDER_TEXT = "Health Slider";
    const string TOWN_TEXT = "Scene1";
    const string MENU_TEXT = "MainMenu";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    protected override void Awake() {
        base.Awake();

        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start() {
        isDead = false;
        currentHealth = maxHealth;
        UpdateHealthSlider();
    }

    private void OnCollisionStay2D(Collision2D other) {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
        EnemyObstacle obstacle = other.gameObject.GetComponent<EnemyObstacle>();

        if (enemy){
            float damage = enemy.attackByEnemy;
            TakeDamage(damage, other.transform);
        }
        else if (obstacle){
            float damage = obstacle.obstacleDamage;
            TakeDamage(damage, other.transform);
            if(obstacle.Poisonous){
                isPoisoned = true;
                StartCoroutine(ApplyPoisonEffect(obstacle));
            }
        }
    }
    public void HealPlayer(int healAmount) {
        currentHealth += healAmount;
        if(currentHealth >=maxHealth){
            currentHealth = maxHealth;
        }
        UpdateHealthSlider();
        // Debug.Log("Health increased: " + currentHealth);
    }
    public void TakeDamage(float damageAmount, Transform hitTransform) {
        if (!canTakeDamage) { return; }

        ScreenShakeManager.Instance.ShakeScreen();
        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        Debug.Log("Player damage ,CurrentHealth: " + currentHealth);
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }
    private void CheckIfPlayerDeath() {
        if (currentHealth <= 0 && !isDead) {
            currentHealth =0;
            isDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);
            currentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine() {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        DontDestroy.SetActive(false);
        SceneManager.LoadScene(MENU_TEXT);
    }

    private IEnumerator DamageRecoveryRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
    private void UpdateHealthSlider() {
        if (healthSlider == null) {
            healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        }
        if(healthText == null){
            healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        healthText.text =currentHealth.ToString();
    }
    public IEnumerator ApplyPoisonEffect(EnemyObstacle obstacle)
    {

        float timePassed = 0f;

        while (timePassed < obstacle.poisonDuration && !isDead)
        {
            yield return new WaitForSeconds(obstacle.poisonInterval);
            TakeDamage(obstacle.poisonDamage,transform);
            timePassed += obstacle.poisonInterval;
        }

        isPoisoned = false;
    }
}
