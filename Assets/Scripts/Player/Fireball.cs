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
        // MouseFollowWithOffset();
        CalculateAttackDirection();
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

    private void CalculateAttackDirection()
{
    Vector3 direction;

    if (Application.isMobilePlatform)
    {
        // Use joystick input for mobile
        Vector2 joystickDirection = JoystickInput.Instance.GetJoystickDirection();
        direction = new Vector3(joystickDirection.x, joystickDirection.y, 0);
    }
    else
    {
        // Use mouse input for PC
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector3 playerPos = PlayerController.Instance.transform.position;
        direction = mouseWorldPos - playerPos;
    }

    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    if (direction.x < 0)
    {
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(180, 0, -angle);
    }
    else
    {
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}

    // private void MouseFollowWithOffset()
    // {

    //     Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     mouseWorldPos.z = 0; 
    //     Vector3 playerPos = PlayerController.Instance.transform.position;
    //     Vector3 direction = mouseWorldPos - playerPos;
    //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    //     if (mouseWorldPos.x < playerPos.x)
    //     {
    //         ActiveWeapon.Instance.transform.rotation = Quaternion.Euler( 180,0 , -angle);
    //     }
    //     else
    //     {
    //         ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
    //     }
    // }
    
}
