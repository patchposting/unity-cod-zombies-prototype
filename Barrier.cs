using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Barrier : MonoBehaviour
{
    public TextMeshProUGUI barrierHealthUI;

    public float barrierHealth = 5f;
    public bool barrierBroken = false;
    public bool buildingBarrier = false;

    private Points pointsScript;
    private RoundSystem roundSystemScript;
    private int delayAmount = 1;
    private float barrierTimer;

    private void Start()
    {
        pointsScript = GameObject.FindWithTag("Player").GetComponent<Points>();
        roundSystemScript = GameObject.FindWithTag("RoundSystem").GetComponent<RoundSystem>();
    }

    void Update()
    {
        barrierHealthUI.text = barrierHealth.ToString("0");

        if (barrierHealth <= 0)
        {
            barrierBroken = true;
        }
        else 
        {
            barrierBroken = false;
        }

        if (!buildingBarrier)
        {
            if (pointsScript.points % 10 != 0)
            {
                pointsScript.points++;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Zombie" && barrierHealth > 0 && other.GetComponent<SeekPlayer>().brokeBarrier == false)
        {
            Debug.Log("Zombie Destroying Barrier");
            barrierHealth -= Time.deltaTime * .75f;
        }
        else if (other.tag == "Player" && barrierHealth < 5)
        {
            Debug.Log("Rebuilding Barrier");
            buildingBarrier = true;
            barrierHealth += Time.deltaTime;

            barrierTimer += (Time.deltaTime);
            if (barrierTimer > delayAmount && pointsScript.barrierPoints < roundSystemScript.maxBarrierPoints)
            {
                barrierTimer = 0;
                pointsScript.barrierPoints++;
                pointsScript.points++;
            }
        }
        else 
        {
            buildingBarrier = false;
        }
    }
}
