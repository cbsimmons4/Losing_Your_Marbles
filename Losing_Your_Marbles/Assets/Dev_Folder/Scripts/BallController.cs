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
    //MapGenerator mg;

    public float wanderRadius;
    public float wanderTimer;
    private Transform target;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //mg = GameObject.Find("Map Generator").GetComponent<MapGenerator>();
        //open_spaces = mg.Get_OS();
        //map = mg.Get_map();
        agent.enabled = true;
        agent.updateRotation = true;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
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