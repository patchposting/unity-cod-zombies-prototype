using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundSystem : MonoBehaviour
{
    public int roundNo = 0;
    public int zombiesForRoundNo = 4;
    public int zombiesToSpawnNo = 4;
    public int zombiesSpawnedNo = 0;
    public int zombieHealth = 100;
    public int barriersAllowedToRebuild = 1;
    public int maxBarrierPoints = 5;
    public float zombieMaxSpeed;

    public bool canSpawn;
    public bool canStartWave;
    public bool resetBarrierPoints;

    public TextMeshProUGUI currentRoundUI;

    public ZombieSpawner zombieSpawnerScript;
    public Zombie zombieScript;
    public Points pointsScript;

    public AudioSource roundSFX;
    public AudioClip roundStart;

    void Start()
    {
        roundSFX.clip = roundStart;
        roundSFX.Play();
        canSpawn = true;
        StartCoroutine(zombieSpawnerScript.SpawnInterval());
    }

    void Update()
    {
        currentRoundUI.text = roundNo.ToString();
        GameObject[] zombiesActiveInScene = GameObject.FindGameObjectsWithTag("Zombie");

        if (zombiesToSpawnNo == 0 && !canSpawn && canStartWave && zombiesActiveInScene.Length == 0)
        {
            StartCoroutine(NextRound());
        }
    }

    public IEnumerator NextRound() 
    {
        canStartWave = false;
        yield return new WaitForSeconds(4);

        roundNo++;
        zombiesForRoundNo+=2;
        zombiesToSpawnNo = zombiesForRoundNo;
        zombiesSpawnedNo = 0;
        zombieHealth += 25;
        maxBarrierPoints += 5;
        pointsScript.barrierPoints = 0;
        zombieMaxSpeed += .25f;
        //zombieScript.health += 10;

        roundSFX.clip = roundStart;
        roundSFX.Play();
        //canStartWave = true;
        canSpawn = true;
        StartCoroutine(zombieSpawnerScript.SpawnInterval());
    }
}
