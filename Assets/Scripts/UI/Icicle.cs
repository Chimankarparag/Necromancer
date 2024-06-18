using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("Icicle Attack");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
