using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    //player health
    public TextMeshProUGUI playerHealthUI;
    public TextMeshProUGUI deathUI;
    public float playerHealth = 3;
    public bool takingDamage = false;
    public int zombiesCloseToPlayer = 0;

    public Image damageRadiationUI;
    public Image damageHealthUI;

    public float damageTimer;
    public float damageDelayAmount;
    public float healTimer;
    public float healDelayAmount;

    public bool isHurting = false;
    public bool isHealing = false;

    public GameObject[] zombies;
    public float radiationDistance = 5;
    public float fadeSpeed;

    void Start()
    {

    }

    void Update()
    {
        playerHealthUI.text = playerHealth.ToString("0") + " HP";

        if (playerHealth <= 0)
        {
            //Time.timeScale = 0;
            //deathUI.text = "YOU DIED";
        }

        // DAMAGE UI
        var tempHealthColor = damageHealthUI.color;
        var tempRadColor = damageRadiationUI.color;

        /*
        zombies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (GameObject zombie in zombies)
        {
            if (Vector3.Distance(transform.position, zombie.transform.position) < radiationDistance)
            {
                Debug.Log("radiation");
                tempRadColor.a += .125f;
                //damageRadiationUI.color = new Color(damageRadiationUI.color.r, damageRadiationUI.color.b, damageRadiationUI.color.g, damageRadiationUI.color.a - fadeSpeed * Time.deltaTime);
                damageRadiationUI.color = tempRadColor;
            }
            else 
            {
                Debug.Log("no radiation");
                tempRadColor.a -= .125f;
                //tempRadColor.a -= Time.deltaTime;
                damageRadiationUI.color = tempRadColor;
            }
        }
        */

        if (playerHealth == 3) 
        {
            tempHealthColor.a = 0;
            damageHealthUI.color = tempHealthColor;
            tempRadColor.a = 0;
            damageRadiationUI.color = tempRadColor;
        }
        else if (playerHealth == 2)
        {
            tempHealthColor.a = .375f;
            damageHealthUI.color = tempHealthColor;
            tempRadColor.a = .375f;
            damageRadiationUI.color = tempRadColor;
        }
        else if (playerHealth == 1)
        {
            tempHealthColor.a = .75f;
            damageHealthUI.color = tempHealthColor;
            tempRadColor.a = .75f;
            damageRadiationUI.color = tempRadColor;
        }
        else if (playerHealth == 0)
        {
            tempHealthColor.a = 1;
            damageHealthUI.color = tempHealthColor;
            tempRadColor.a = 1;
            damageRadiationUI.color = tempRadColor;

            Time.timeScale = 0;
            deathUI.text = "YOU DIED";
        }

        // DAMAGE
        if (zombiesCloseToPlayer > 0 && playerHealth > 0)
        {
            Hurt();
            //playerHealth -= 1 * Time.deltaTime;
        }
        else if (zombiesCloseToPlayer == 0 && playerHealth > 0 && playerHealth < 3)
        {
            Heal();
            //playerHealth += 1 * Time.deltaTime;
        }
        else 
        { 
            damageTimer = 0; 
            healTimer = 0; 
        }
    }

    public void Hurt()
    {
        damageTimer += Time.deltaTime;
        if (damageTimer > damageDelayAmount)
        {
            damageTimer = 0;
            playerHealth--;
            //isHurting = false;
        }
    }

    public void Heal()
    {
        healTimer += Time.deltaTime;
        if (healTimer > healDelayAmount)
        {
            healTimer = 0;
            playerHealth++;
            //isHealing = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Zombie") 
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
