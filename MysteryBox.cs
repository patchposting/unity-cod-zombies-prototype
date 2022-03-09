using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    public GameObject[] weaponsInBox;
    public int selectedGun;
    public Transform gunCyclePos;

    private float timer;
    int counter, counterCompare;

    public EnvironmentItems environmentItemsScript;

    public float cycleTimer;
    public float delayAmount;
    public int currentIndex = 0;

    public float cycleTime;

    void Start()
    {
        
    }

    public void FixedUpdate()
    {
        if (environmentItemsScript.mysteryBoxOpen)
        {
            Debug.Log("cycle through weapons");
            RandomizeWeapon();

            cycleTime += Time.deltaTime;
            if (cycleTime > 3.75f) 
            {
                delayAmount += Time.deltaTime / 10;
            }

            /*
            timer += Time.deltaTime;

            if (timer < 3 && counter < counterCompare)
            {
                counter++;
            }
            else if (counter == counterCompare)
            {
                counter = 0;
                RandomizeWeapon();
                counterCompare++;
            }

            weaponsInBox[selectedGun].transform.position = gunCyclePos.localPosition;
            */
        }
        else 
        {
            cycleTime = 0;
            delayAmount = .125f;
            //counter = 0;
            //counterCompare = 0;
            //timer = 0;
        }
    }

    public void RandomizeWeapon() 
    {
        cycleTimer += Time.deltaTime;
        if (cycleTimer > delayAmount && (delayAmount < .375f)) 
        {
            int newIndex = Random.Range(0, weaponsInBox.Length);
            weaponsInBox[currentIndex].SetActive(false);
            if (newIndex == currentIndex)
            {
                //newIndex++;
                RandomizeWeapon();
            }
            else 
            {
                currentIndex = newIndex;
                for (int i = 0; i < weaponsInBox.Length; i++) 
                {
                    if (currentIndex != i)
                    {
                        weaponsInBox[i].SetActive(false);
                    }
                    weaponsInBox[currentIndex].SetActive(true);
                }
                //currentIndex = newIndex;
                //weaponsInBox[currentIndex].SetActive(true);
            }

            //weaponsInBox[Random.Range(0, weaponsInBox.Length)].SetActive(true);
            cycleTimer = 0;
        }


        /*
        int gunCount = weaponsInBox.Length;
        int rand = Random.Range(0, gunCount);

        while (rand == selectedGun) 
        {
            Random.Range(0, gunCount);
        }
        selectedGun = rand;

        for (int i = 0; i < gunCount; i++) 
        {
            weaponsInBox[i].SetActive(false);
        }

        weaponsInBox[selectedGun].SetActive(true);
        weaponsInBox[selectedGun].transform.position = gunCyclePos.localPosition;
        */
    }
}
