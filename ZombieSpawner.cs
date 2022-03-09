using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public Transform[] zombieSpawners;
    public GameObject zombie;
    public GameObject zombieHolder;

    public RoundSystem roundSystemScript;

    public void SpawnZombies() 
    {
        if (roundSystemScript.zombiesToSpawnNo > 0 && roundSystemScript.canSpawn)
        {
            Debug.Log("Spawned Zombie");

            Vector3 posAbove = new Vector3(0, 1, 0);
            Transform spawnPoint = zombieSpawners[Random.Range(0, zombieSpawners.Length)];
            Instantiate(zombie, spawnPoint.transform.position + posAbove, Quaternion.identity, zombieHolder.transform);
            zombie.gameObject.tag = "Zombie";
            //zombie.SetActive(true);
            roundSystemScript.zombiesSpawnedNo++;
            roundSystemScript.zombiesToSpawnNo--;

            StartCoroutine(SpawnInterval());
        }
    }

    public IEnumerator SpawnInterval() 
    {
        yield return new WaitForSeconds(1);

        if (roundSystemScript.zombiesToSpawnNo != 0)
        {
            SpawnZombies();
        }
        else 
        {
            roundSystemScript.canSpawn = false;
            roundSystemScript.canStartWave = true;
        }
    }
}