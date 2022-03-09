using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private PlayerPowerUp playerPowerUpScript;

    void Start()
    {
        playerPowerUpScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPowerUp>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerPowerUpScript.InstaKillCall();
            Destroy(this.gameObject);
        }
    }
}
