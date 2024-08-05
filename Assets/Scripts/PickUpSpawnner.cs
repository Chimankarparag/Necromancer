using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawnner : MonoBehaviour
{
    [SerializeField] private GameObject SoulPrefab,HealthPrefab,ManaGlobe,SmallMana;
    [SerializeField] private bool isEnemy = false;

    [SerializeField] private  bool isRoot = false;

    [SerializeField] private  bool isManaEnemy = false;
    public void DropItems() {

        if(isEnemy) {
            Instantiate(SoulPrefab, transform.position, Quaternion.identity); 
        }else if(isRoot) {
            Instantiate(SmallMana, transform.position, Quaternion.identity); 
            //can make it a chance event by giving random number but since the game has about half of them as not killable so kept as it is
        }
        else if(isManaEnemy){
            Instantiate(ManaGlobe, transform.position, Quaternion.identity); 
        }
        else{
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
