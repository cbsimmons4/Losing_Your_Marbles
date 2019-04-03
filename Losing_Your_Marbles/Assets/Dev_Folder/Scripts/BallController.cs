using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BallController : MonoBehaviour
{
    List<KeyValuePair<int, int>> open_spaces;
    Vector3 destination;
    int[,] map;
    NavMeshAgent agent;
    MapGenerator mg;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mg = GameObject.Find("Map Generator").GetComponent<MapGenerator>();
        open_spaces = mg.Get_OS();
        map = mg.Get_map();
        agent.enabled = true;
        this.PickNewDistination();
    }
    void PickNewDistination()
    {
        KeyValuePair<int, int> space = open_spaces[Random.Range(0, open_spaces.Count)];
        this.destination = new Vector3((space.Key - mg.width/2) - 1 , 0, (space.Value - mg.height/2) - 1);
        agent.SetDestination(this.destination);
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0,100) < 0.005f || Input.GetKey(KeyCode.P)|| 
            agent.path.status == NavMeshPathStatus.PathInvalid || 
            Vector3.Distance(agent.transform.position, destination) < 1) {
            this.PickNewDistination();
        }
    }

}