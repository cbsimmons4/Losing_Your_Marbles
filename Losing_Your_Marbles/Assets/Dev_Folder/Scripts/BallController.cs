using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BallController : MonoBehaviour
{
    //List<KeyValuePair<int, int>> open_spaces;
    //Vector3 destination;
    //int[,] map;
    NavMeshAgent agent;
    Rigidbody rb;
    Animator anim;
    //MapGenerator mg;

    public float wanderRadius;
    public float wanderTimer;
    //public GameObject player;
    private Transform target;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //mg = GameObject.Find("Map Generator").GetComponent<MapGenerator>();
        //open_spaces = mg.Get_OS();
        //map = mg.Get_map();
        agent.enabled = true;
        agent.updateRotation = true;
     //   agent.updatePosition = false;

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            anim.SetBool("isRolling", true);
            agent.SetDestination(newPos);
            timer = 0;
            anim.SetBool("isRolling", false);
        }
    }


    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }

}