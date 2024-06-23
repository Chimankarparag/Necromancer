using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawnner : MonoBehaviour
{
    [SerializeField] private GameObject SoulPrefab,HealthPrefab,ManaGlobe;
    [SerializeField] private bool isEnemy = false;
    public void DropItems() {

        if(isEnemy) {
            Instantiate(SoulPrefab, transform.position, Quaternion.identity); 
            Debug.Log("Instantiating SoulPrefab");
        }else{
            int randomNum = Random.Range(1, 4);

            if (randomNum == 1) {
                Instantiate(HealthPrefab, transform.position, Quaternion.identity); 
            } 

            if (randomNum == 2) {
                Instantiate(ManaGlobe, transform.position, Quaternion.identity); 
            }
        }
    }
}
