using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject ice1prefab;
    [SerializeField] private Transform ice1spawnpoint;
    private Animator myAnimator;
    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private void Awake(){
        myAnimator = GetComponent<Animator>();
    }

    private void Update() {
        MouseFollowWithOffset();
    }

    public void Attack()
    {
        myAnimator.SetTrigger(FIRE_HASH);
        GameObject newice = Instantiate(ice1prefab,ice1spawnpoint.position,ActiveWeapon.Instance.transform.rotation);
        newice.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
    }
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
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
