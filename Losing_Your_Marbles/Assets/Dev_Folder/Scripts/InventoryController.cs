using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject player;
    public Transform trap;
    public Text trapCount;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddTrap()
    {
        int trapCt = int.Parse(trapCount.text);
        if (trapCt > 0)
        {
            Instantiate(this.trap, player.transform.position-new Vector3(0,0.25f,0), Quaternion.identity);
            trapCount.text = (trapCt - 1).ToString();
        }
    }
}
