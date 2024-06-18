using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawnner : MonoBehaviour
{
    [SerializeField] private GameObject SoulPrefab;

    public void DropItems() {
        Instantiate(SoulPrefab, transform.position, Quaternion.identity);
    }
}
