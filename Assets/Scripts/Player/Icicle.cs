using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject ice1prefab;
    [SerializeField] private Transform ice1spawnpoint;
    private Animator myAnimator;
    private float manaUsage;
    private float manaLeft;
    readonly int FIRE_HASH = Animator.StringToHash("Fire");


    private void Awake(){
        myAnimator = GetComponent<Animator>();
    }
    private void Start() {
        manaUsage = GetWeaponInfo().manaUsage;
    }

    private void Update() {
        MouseFollowWithOffset();
    }

    public void Attack()
    {
        manaLeft = PlayerMana.Instance.currentMana - manaUsage;
        if(manaLeft >=0 ){
            PlayerMana.Instance.UseMana(manaUsage);
            myAnimator.SetTrigger(FIRE_HASH);
            GameObject newice = Instantiate(ice1prefab,ice1spawnpoint.position,ActiveWeapon.Instance.transform.rotation);
            newice.GetComponent<Projectile>().UpdateProjectileRange(weaponInfo.weaponRange);

        }
        else{
            Debug.Log("Insufficient Mana");
        }
        
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


    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }
}
