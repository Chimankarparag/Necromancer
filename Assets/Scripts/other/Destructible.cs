using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;
    [SerializeField] private bool isEnemyObstacle = false;
    private void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.GetComponent<DamageSource>() && !isEnemyObstacle){//||other.gameObject.GetComponent<Projectile>() ){

            GetComponent<PickUpSpawnner>().DropItems();
            Instantiate(destroyVFX, transform.position , Quaternion.identity);
            Destroy(gameObject);

        }
    }
}
