using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float attackRange = 2.5f;
    public float health = 100;
    public int powerUpChanceNo;

    private PlayerHealth playerHealthScript;
    private Points pointsScript;
    private PlayerPowerUp playerPowerUpScript;
    private PowerUpSpawner powerUpSpawnerScript;
    private Gun gunScript;
    private RoundSystem roundSystemScript;

    //power-up drops
    //public PowerUp powerUpScript;
    public GameObject powerUp;
    //public GameObject powerUpHolder;
    public int powerUpType;

    public AudioSource zombieSFX;
    public AudioClip zombieSpawn;

    public Vector3 zombieDeathLocation;

    public bool nearPlayer;

    private void Start()
    {
        //powerUp.SetActive(false);
        zombieSFX.clip = zombieSpawn;
        zombieSFX.Play();

        playerHealthScript = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        pointsScript = GameObject.FindWithTag("Player").GetComponent<Points>();
        playerPowerUpScript = GameObject.FindWithTag("Player").GetComponent<PlayerPowerUp>();
        powerUpSpawnerScript = GameObject.FindWithTag("Power-Up Spawner").GetComponent<PowerUpSpawner>();
        gunScript = GameObject.FindWithTag("Gun").GetComponent<Gun>();
        roundSystemScript = GameObject.FindWithTag("RoundSystem").GetComponent<RoundSystem>();

        health = roundSystemScript.zombieHealth;
        //health = 100;
    }

    public void Update()
    {
        gunScript = GameObject.FindWithTag("Gun").GetComponent<Gun>();
    }

    public void TakeDamageHead(float amount)
    {
        health -= amount;

        if (health <= 0 || playerPowerUpScript.instakillActive) 
        {
            gunScript.KillMarkerActive();
            pointsScript.points += 100;
            Destroy(this.gameObject);
        }
    }

    public void TakeDamageTorso(float amount)
    {
        health -= amount;

        if (health <= 0 || playerPowerUpScript.instakillActive)
        {
            gunScript.KillMarkerActive();
            pointsScript.points += 50;
            Destroy(this.gameObject);
        }
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !nearPlayer)
        {
            nearPlayer = true;
            playerHealthScript.zombiesCloseToPlayer++;
        }
        else
        {
            nearPlayer = false;
            playerHealthScript.zombiesCloseToPlayer++;
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            nearPlayer = true;
            playerHealthScript.zombiesCloseToPlayer++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            nearPlayer = false;
            playerHealthScript.zombiesCloseToPlayer--;
        }
    }

    /*
private void OnTriggerEnter(Collider other)
{
    if (other.tag == "Player")
    {
        playerHealthScript.zombiesCloseToPlayer++;
    }
}

private void OnTriggerExit(Collider other)
{
    if (other.tag == "Player")
    {
        playerHealthScript.zombiesCloseToPlayer--;
    }
}
*/

    private void OnDestroy()
    {
        if (nearPlayer) 
        {
            Debug.Log("not close to player");
            nearPlayer = false;
            playerHealthScript.zombiesCloseToPlayer--;
        }

        zombieDeathLocation = this.gameObject.transform.position;
        powerUpSpawnerScript.powerUpSpawnPoint = zombieDeathLocation;

        powerUpSpawnerScript.SpawnPowerUp();
    }

    public void InstaKill()
    {

    }

    public void MaxAmmo()
    {

    }
}
