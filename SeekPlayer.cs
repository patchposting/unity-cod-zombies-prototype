using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SeekPlayer : MonoBehaviour
{
    private NavMeshAgent zombie;
    private Transform player;
    private Barrier barrierScript;
    private RoundSystem roundSystemScript;

    public float minZombieSpeed;
    public float maxZombieSpeed;
    public float defaultSpeed;
    public bool brokeBarrier = false;

    void Start()
    {
        zombie = this.gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;

        zombie.speed = Random.Range(minZombieSpeed, maxZombieSpeed);
        defaultSpeed = zombie.speed;
        zombie.avoidancePriority = (Random.Range(1, 10));

        //wont work for big rounds, fix later
        roundSystemScript = GameObject.FindWithTag("RoundSystem").GetComponent<RoundSystem>();
        maxZombieSpeed += roundSystemScript.zombieMaxSpeed;
    }

    void Update()
    {
        zombie.SetDestination(player.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Barrier" && !brokeBarrier) 
        {
            barrierScript = other.GetComponent<Barrier>();

            if (barrierScript.barrierBroken == false)
            {
                Debug.Log("Zombie Stopped");
                zombie.speed = 0;
            }
            else 
            { 
                zombie.speed = defaultSpeed; Debug.Log("Zombie Resumed");
                brokeBarrier = true;
            }
        }

        if (other.tag == "Barrier" && brokeBarrier) 
        {
            //zombie.speed = defaultSpeed - .5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            zombie.speed = .125f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Barrier" && brokeBarrier)
        {
            //zombie.speed = defaultSpeed + .5f;
        }

        if (other.tag == "Player")
        {
            zombie.speed = defaultSpeed;
        }
    }
}
