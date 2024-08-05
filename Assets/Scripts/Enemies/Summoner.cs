using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour
{

    public GameObject spawnEnemy1;

    public GameObject spawnEnemy2;

    [SerializeField] private float spawn2count =2;

    public float spawnInterval = 5f;

    public Vector3 spawnOffset = new Vector3(1f, 0f, 0f);

    private bool isAlive = true;
    public int maxUnits = 5;

    private List<GameObject> activeEnemies = new List<GameObject>();

    public void StartSpawn()
    {
        StartCoroutine(SpawnEnemy());
    }

    public IEnumerator SpawnEnemy()
    {
        while (isAlive)
        {

            if (activeEnemies.Count < maxUnits)
            {
                GameObject enemy1 = Instantiate(spawnEnemy1, transform.position + spawnOffset, Quaternion.identity);
                enemy1.GetComponent<EnemyHealth>().OnDeath += () => OnEnemyDeath(enemy1);
                activeEnemies.Add(enemy1);

                for (int i = 0; i < spawn2count; i++)
                {
                    if (activeEnemies.Count < maxUnits)
                    {
                        GameObject enemy2 = Instantiate(spawnEnemy2, transform.position + spawnOffset, Quaternion.identity);
                        enemy2.GetComponent<EnemyHealth>().OnDeath += () => OnEnemyDeath(enemy2);
                        activeEnemies.Add(enemy2);
                    }
                }
            }


            Debug.Log("Enemy Spawned");

            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    private void OnEnemyDeath(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        Destroy(enemy);
    }

    private void OnDestroy()
    {

        isAlive = false;
    }
}
