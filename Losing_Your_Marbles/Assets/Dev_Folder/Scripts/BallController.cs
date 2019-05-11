using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BallController : MonoBehaviour
{
    NavMeshAgent agent;
    Rigidbody rb;
   
    public float wanderRadius;
    public float wanderTimer;
    private PlayerController playerCon;
    private Transform target;
    private float timer;
    private GameObject player;
    Vector3 lastPosition;
    float speed;

    public List<Material> materials;


    void Start()
    {
        player = GameObject.Find("Player");
        this.GetComponent<Renderer>().material = this.materials[Random.Range(0, materials.Count)];
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        agent.enabled = true;
        agent.updateUpAxis = false;
        lastPosition = transform.position;
        if (GameObject.Find("Player") != null)
        {

            GameObject.Find("Player").GetComponent<PlayerController>().incrementMarbleCount();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
     
        if (agent.enabled && timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

    }

    private void FixedUpdate()
    {
        Vector3 direction = Vector3.Cross(lastPosition - transform.position, Vector3.up);
        speed = (((transform.position - lastPosition).magnitude) / Time.deltaTime);
        transform.RotateAround(transform.position, direction, speed);
        lastPosition = transform.position;
       
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }
}