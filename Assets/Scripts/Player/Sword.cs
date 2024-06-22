 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sword : MonoBehaviour,IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    private Transform weaponCollider;
    // [SerializeField]private float swordAttackCD = 0.5f;
    //in AttackCdRoutine replace the number by swordattackCD

    private Animator myAnimator;
    // private PlayerController playerController;
    // private ActiveWeapon activeWeapon;

     

    private void Awake(){
        // playerController = GetComponentInParent<PlayerController>();
        // activeWeapon = GetComponentInParent<ActiveWeapon>(); 
        myAnimator = GetComponent<Animator>();
       
    }
        private void Update() {
        MouseFollowWithOffset();
    }
    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }

  
    void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        weaponCollider.gameObject.SetActive(false);    //Adjustment to the bug ,line 
    }

    public void Attack() {

        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);  
        StartCoroutine(AttackCDRoutine());
        

    }

    private IEnumerator AttackCDRoutine(){
        yield return new WaitForSeconds(0.5f); 
    }
    private void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }
    private void MouseFollowWithOffset() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x) {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0,-180,0);
        } else {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }


}
