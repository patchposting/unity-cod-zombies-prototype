using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpSpawner : MonoBehaviour
{
    public TextMeshProUGUI powerUpUI;

    public int powerUpChanceNo;
    public GameObject powerUp;
    public GameObject powerUpHolder;
    public int powerUpType;

    public Vector3 powerUpSpawnPoint;

    public void SpawnPowerUp() 
    {
        powerUpChanceNo = Random.Range(0, 100);

        if (powerUpChanceNo < 10)
        {
            powerUpType = Random.Range(0, 2);
            if (powerUpType == 0)
            {
                Destroy(Instantiate(powerUp, powerUpSpawnPoint, Quaternion.identity, powerUpHolder.gameObject.transform), 8);
                powerUp.SetActive(true);
                //Destroy(powerUp, 8);
            }
            else if (powerUpType == 1)
            {
                Destroy(Instantiate(powerUp, powerUpSpawnPoint, Quaternion.identity, powerUpHolder.gameObject.transform), 8);
                powerUp.SetActive(true);
                //Destroy(powerUp, 8);
            }
        }
    }

    /*
    private void OnDestroy()
    {
        powerUpUI.text = "POWER-UP";
        StartCoroutine(PowerUpUI());
    }

    IEnumerator PowerUpUI() 
    {
        yield return new WaitForSeconds(4);
        powerUpUI.text = "";
    }
    */
}
