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
    //Animator anim;
    //MapGenerator mg;

    public float wanderRadius;
    public float wanderTimer;
    private PlayerController playerCon;
    private Transform target;
    private float timer;
   
    Vector3 lastPosition;
    float speed;

    public List<Material> materials;


    void Start()
    {
        //GameObject.Find("Player").GetComponent<PlayerController>().incrementMarbleCount();
        this.GetComponent<Renderer>().material = this.materials[Random.Range(0, materials.Count)];
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
        //mg = GameObject.Find("Map Generator").GetComponent<MapGenerator>();
        //open_spaces = mg.Get_OS();
        //map = mg.Get_map();
        agent.enabled = true;
        //agent.updateRotation = false;
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
     
        if (timer >= wanderTimer)
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