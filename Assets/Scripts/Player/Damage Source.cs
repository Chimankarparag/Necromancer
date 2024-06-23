using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private int damageAmount;

    private void Start() {
        MonoBehaviour currenActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        // Debug.Log(currenActiveWeapon);
        damageAmount = (currenActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
        if(damageAmount==0) damageAmount = 1;
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        enemyHealth?.TakeDamage(damageAmount);
    }
}

