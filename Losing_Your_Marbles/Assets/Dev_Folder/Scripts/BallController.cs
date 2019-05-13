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
    private MapGenerator gm;
    private List<KeyValuePair<int, int>> open_spaces;
    private bool isRunning;


    public List<Material> materials;


    void Start()
    {
        isRunning = false;
        gm = GameObject.Find("Map Generator").GetComponent <MapGenerator>();
        this.open_spaces = gm.Get_OS();
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

        if (!isRunning)
        {
            timer += Time.deltaTime;


            if (agent.enabled && timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("marbletrigger"))
        {
            isRunning = true;
            KeyValuePair<int, int> cur = this.open_spaces[Random.Range(0, this.open_spaces.Count)];
            Vector3 cur_vec = new Vector3((cur.Key - gm.GetCenterX()) * 2, 0, (cur.Value - gm.GetCenterZ()) * 2);
            while (Vector3.Distance(player.transform.position, cur_vec) < 40) {
                cur = this.open_spaces[Random.Range(0, this.open_spaces.Count)];
                cur_vec = new Vector3((cur.Key - gm.GetCenterX()) * 2, 0, (cur.Value - gm.GetCenterZ()) * 2);
            }
            agent.SetDestination(cur_vec);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("marbletrigger"))
        {
            isRunning = false;

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