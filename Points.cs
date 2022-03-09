using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
    //points
    public TextMeshProUGUI pointsUI;
    public int points = 750;
    public Gun gunScript;
    public WeaponSwitching weaponSwitchingScript;

    public int gun0AmmoCost = 250;

    public int gun1Cost = 1250;
    public int gun1AmmoCost = 500;

    public bool boughtGun1 = false;

    public GameObject gun0;
    public GameObject gun1;
    public Transform weaponHolder;

    public int barrierPoints;

    void Update()
    {
        pointsUI.text = points.ToString();

        gunScript = GameObject.FindWithTag("Gun").GetComponent<Gun>();
    }

    private void OnTriggerStay(Collider other)
    {
        //GUN-0
        if (other.tag == "WB GUN-0" && points >= gun0AmmoCost && gun0.activeSelf && gunScript.currentAmmo != gunScript.maxAmmo && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("bought gun-0");
            points -= gun0AmmoCost;
            gunScript.currentAmmo = gunScript.maxAmmo;
        }
        //GUN-1
        else if (other.tag == "WB GUN-1" && Input.GetKeyDown(KeyCode.E))
        {
            if (!boughtGun1 && points >= gun1Cost)
            {
                Debug.Log("bought gun-1");
                points -= gun1Cost;
                //gunScript.currentAmmo = gunScript.maxAmmo;
                gun0.GetComponent<Gun>().currentAmmo = gun0.GetComponent<Gun>().maxAmmo;

                gun1.transform.SetParent(weaponHolder, true);
                gun1.transform.localPosition = new Vector3(.25f, 0, 1);
                gun0.SetActive(false);
                gun1.SetActive(true);
                weaponSwitchingScript.selectedWeapon += 1;
                boughtGun1 = true;
            }
            else if(boughtGun1 && points >= gun1AmmoCost && gun1.activeSelf && gunScript.currentAmmo != gunScript.maxAmmo)
            {
                Debug.Log("bought gun-1 ammo");
                points -= gun1AmmoCost;
                gun1.GetComponent<Gun>().currentAmmo = gun1.GetComponent<Gun>().maxAmmo;
            }
        }
    }
}
