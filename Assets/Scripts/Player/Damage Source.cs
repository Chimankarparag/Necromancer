using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private int damageAmount;

    private void Start() {
        MonoBehaviour currenActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        Debug.Log(currenActiveWeapon);
        // damageAmount = (currenActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
        // Debug.Log("Previous" + damageAmount);
        
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        MonoBehaviour currenActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        damageAmount = (currenActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        // if(damageAmount==0) damageAmount = 1;
        // Debug.Log("now" + damageAmount);
        enemyHealth?.TakeDamage(damageAmount);
    }
}

