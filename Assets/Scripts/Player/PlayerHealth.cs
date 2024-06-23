using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private float currentHealth;
    private Slider healthSlider;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;
    const string HEALTH_SLIDER_TEXT = "Health Slider";

    protected override void Awake() {
        base.Awake();

        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start() {
        currentHealth = maxHealth;
        UpdateHealthSlider();
    }

    private void OnCollisionStay2D(Collision2D other) {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();


        if (enemy) {
            float damage = enemy.attackByEnemy;
            TakeDamage(damage, other.transform);
        }
    }
    public void HealPlayer(int healAmount) {
        currentHealth += healAmount;
        if(currentHealth >=maxHealth){
            currentHealth = maxHealth;
        }
        UpdateHealthSlider();
        Debug.Log("Health increased: " + currentHealth);
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
        if (currentHealth <= 0) {
            currentHealth = 0;
            Debug.Log("Player Death");
        }
    }

    private IEnumerator DamageRecoveryRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
    private void UpdateHealthSlider() {
        if (healthSlider == null) {
            healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
