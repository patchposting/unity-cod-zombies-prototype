using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public GameObject doorA1;
    public GameObject doorA2;
    public GameObject doorA3;
    public GameObject doorB1;

    public int doorA1Cost;
    public int doorA2Cost;
    public int doorA3Cost;
    public int doorB1Cost;

    private Points pointsScript;

    void Start()
    {
        pointsScript = GameObject.FindWithTag("Player").GetComponent<Points>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Door" && Input.GetKeyDown(KeyCode.E))
        {
            //StartingRoom
            if (other.gameObject.name == "DoorA1" && pointsScript.points >= doorA1Cost)
            {
                Debug.Log("opened doorA1");
                Destroy(doorA1);
                pointsScript.points -= doorA1Cost;
            }
            else if (other.gameObject.name == "DoorA2" && pointsScript.points >= doorA2Cost)
            {
                Debug.Log("opened doorA2");
                Destroy(doorA2);
                pointsScript.points -= doorA2Cost;
                //boughtDoorA2 = true;
            }
            else if (other.gameObject.name == "DoorA3" && pointsScript.points >= doorA3Cost)
            {
                Debug.Log("opened doorA3");
                Destroy(doorA3);
                pointsScript.points -= doorA3Cost;
            }
            else if (other.gameObject.name == "DoorB1" && pointsScript.points >= doorB1Cost)
            {
                Debug.Log("opened doorB1");
                Destroy(doorB1);
                pointsScript.points -= doorB1Cost;
            }
        }
    }
}
