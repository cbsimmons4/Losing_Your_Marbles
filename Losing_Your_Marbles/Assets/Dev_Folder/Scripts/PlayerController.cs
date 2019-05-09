using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject FreezeGun;
    public GameObject OpenCH;
    public GameObject ClosedCH;
    public Text marbleCount;
    public Text winText;
    public Text healthRemain;
    public Transform trap;

    int selected;
   
    private void Awake()
    {
        marbleCount = GameObject.Find("Marble Count").GetComponent<Text>();
        winText = GameObject.Find("Win Text").GetComponent<Text>();
        healthRemain = GameObject.Find("Health Remain").GetComponent<Text>();
        selected = 1;
        GameObject.Find("Ammo Title").GetComponent<Text>().color = new Color(255, 215, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        FreezeGun.transform.position = FreezeGun.transform.parent.position + FreezeGun.transform.parent.right + (1.3f * FreezeGun.transform.parent.forward) + (-0.6f * FreezeGun.transform.parent.up);
        this.OpenCH.SetActive(true);
        this.ClosedCH.SetActive(false);
       // marbleCount = GameObject.Find("Marble Count").GetComponent<Text>();
    }

    public void incrementMarbleCount()
    {
        marbleCount.text = (int.Parse(marbleCount.text) + 1).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            FreezeGun.transform.position = FreezeGun.transform.parent.position + (1.3f * FreezeGun.transform.parent.forward) + (-0.6f * FreezeGun.transform.parent.up);
            this.OpenCH.SetActive(false);
            this.ClosedCH.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            FreezeGun.transform.position = FreezeGun.transform.parent.position + FreezeGun.transform.parent.right + (1.3f * FreezeGun.transform.parent.forward) + (-0.6f * FreezeGun.transform.parent.up);
            this.OpenCH.SetActive(true);
            this.ClosedCH.SetActive(false);
        }

        if (Input.GetKey(KeyCode.R))
        {
            FreezeGun.transform.position = FreezeGun.transform.parent.position + FreezeGun.transform.parent.right + (1.3f * FreezeGun.transform.parent.forward) + (-0.6f * FreezeGun.transform.parent.up);
            this.OpenCH.SetActive(true);
            this.ClosedCH.SetActive(false);
        }

        if (Input.GetKey(KeyCode.G))
        {
            Collider[] marbles = Physics.OverlapSphere(FreezeGun.transform.position, 2);
            foreach(Collider m in marbles)
            {
                if (m.CompareTag("marble"))
                {
                    DestroyImmediate(m.gameObject);
                    marbleCount.text = (int.Parse(marbleCount.text) - 1).ToString();
                    if(int.Parse(marbleCount.text) == 0)
                    {
                        winText.text = "You Win!";
                    }
                }
                   
            }
        }
        // Keys 1-6 control inventory
        if (Input.GetKey(KeyCode.Alpha1))
        {
            selected = 1;
            GameObject.Find("Ammo Title").GetComponent<Text>().color = new Color(255, 215, 0);
            GameObject.Find("Traps Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Invisibility Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Speed Boost Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Freeze Gun Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Health Potion Title").GetComponent<Text>().color = new Color(255, 255, 255);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            selected = 2;
            GameObject.Find("Ammo Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Traps Title").GetComponent<Text>().color = new Color(255, 215, 0);
            GameObject.Find("Invisibility Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Speed Boost Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Freeze Gun Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Health Potion Title").GetComponent<Text>().color = new Color(255, 255, 255);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            selected = 3;
            GameObject.Find("Ammo Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Traps Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Invisibility Title").GetComponent<Text>().color = new Color(255, 215, 0);
            GameObject.Find("Speed Boost Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Freeze Gun Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Health Potion Title").GetComponent<Text>().color = new Color(255, 255, 255);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            selected = 4;
            GameObject.Find("Ammo Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Traps Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Invisibility Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Speed Boost Title").GetComponent<Text>().color = new Color(255, 215, 0);
            GameObject.Find("Freeze Gun Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Health Potion Title").GetComponent<Text>().color = new Color(255, 255, 255);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            selected = 5;
            GameObject.Find("Ammo Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Traps Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Invisibility Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Speed Boost Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Freeze Gun Title").GetComponent<Text>().color = new Color(255, 215, 0);
            GameObject.Find("Health Potion Title").GetComponent<Text>().color = new Color(255, 255, 255);
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            selected = 6;
            GameObject.Find("Ammo Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Traps Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Invisibility Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Speed Boost Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Freeze Gun Title").GetComponent<Text>().color = new Color(255, 255, 255);
            GameObject.Find("Health Potion Title").GetComponent<Text>().color = new Color(255, 215, 0);
        }
        // Left click to use from inventory
        if (Input.GetMouseButton(0))
        {
            switch (selected)
            {
                case 1:
                    Text ammoCt = GameObject.Find("Traps Count").GetComponent<Text>();
                    if (int.Parse(ammoCt.text) > 0)
                    {
                        // shoot from gun
                    }

                    break;
                case 2:
                    Text trapCt = GameObject.Find("Traps Count").GetComponent<Text>();
                    if (int.Parse(trapCt.text) > 0)
                    {
                        Instantiate(this.trap, this.transform.position - new Vector3(0, 0.25f, 0), Quaternion.identity);
                        trapCt.text = (int.Parse(trapCt.text) - 1).ToString();
                    }
                    break;
                case 3:
                    Text invCt = GameObject.Find("Invisibility Count").GetComponent<Text>();
                    if (int.Parse(invCt.text) > 0)
                    {
                        // turn enemy off
                        invCt.text = (int.Parse(invCt.text) - 1).ToString();
                    }
                    break;
                case 4:
                    Text speedCt = GameObject.Find("Speed Boost Count").GetComponent<Text>();
                    if (int.Parse(speedCt.text) > 0)
                    {
                        //increase speed for period of time
                        speedCt.text = (int.Parse(speedCt.text) - 1).ToString();
                    }
                    break;
                case 5:
                    Text freezeCt = GameObject.Find("Freeze Gun Count").GetComponent<Text>();
                    if (int.Parse(freezeCt.text) > 0)
                    {
                        //increase speed for period of time
                        freezeCt.text = (int.Parse(freezeCt.text) - 1).ToString();
                    }
                    break;
                case 6:
                    Text hpCt = GameObject.Find("Health Potion Count").GetComponent<Text>();
                    if (int.Parse(hpCt.text) > 0)
                    {
                        healthRemain.text = 5.ToString();
                        hpCt.text = (int.Parse(hpCt.text) - 1).ToString();
                    }
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
      if(other.CompareTag("mystery box"))
        {

            int rand = Random.Range(0, 6);
            incrementItemCount(rand);
            Destroy(other.gameObject); 
        }  
    }
    
    void incrementItemCount(int index)
    {
        Text count;
        switch (index)
        {
            case 0:
                count = GameObject.Find("Ammo Count").GetComponent<Text>();
                count.text = (int.Parse(count.text) + Random.Range(9,25)).ToString();
                break;
            case 1:
                count = GameObject.Find("Traps Count").GetComponent<Text>();
                break;
            case 2:
                count = GameObject.Find("Invisibility Count").GetComponent<Text>();
                break;
            case 3:
                count = GameObject.Find("Speed Boost Count").GetComponent<Text>();
                break;
            case 4:
                count = GameObject.Find("Freeze Gun Count").GetComponent<Text>();
                break;
            case 5:
                count = GameObject.Find("Health Potion Count").GetComponent<Text>();
                break;
            default:
                goto case 0;
        }
        count.text = (int.Parse(count.text) + 1).ToString();
    }

    public void decrementHealth()
    {
           //get the current health
        int curr = int.Parse(healthRemain.text);
        //decrement the health by one
        if (curr != 0)
        {
            curr--;
            healthRemain.text = curr.ToString();
        }

        //check if health drops to 0 
        if(curr == 0)
        {
            winText.text = "You Lose!";
            //add some operation, possible back to the main menu
        }
    }

}
