using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;

    [SerializeField] private GameObject fireMagicprefab;
    [SerializeField] private Transform fireMagicSpawnPoint;

    private Animator myAnimator;
    private float manaUsage;
    private float manaLeft;
    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }
    private void Start() {
        manaUsage = GetWeaponInfo().manaUsage;
    }
    private void Update() {
        MouseFollowWithOffset();
    }


    public void Attack() {
        manaLeft = PlayerMana.Instance.currentMana - manaUsage;
        if(manaLeft >=0 ){
            PlayerMana.Instance.UseMana(manaUsage);
            myAnimator.SetTrigger(ATTACK_HASH);
            GameObject newfiremagic = Instantiate(fireMagicprefab,fireMagicSpawnPoint.position,ActiveWeapon.Instance.transform.rotation);
            newfiremagic.GetComponent<Projectile>().UpdateProjectileRange(weaponInfo.weaponRange);
        }
        else{
            Debug.Log("Insufficient Mana");
        }
    }
    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }
    private void MouseFollowWithOffset()
    {

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0; 
        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 direction = mouseWorldPos - playerPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (mouseWorldPos.x < playerPos.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler( 180,0 , -angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    
}
