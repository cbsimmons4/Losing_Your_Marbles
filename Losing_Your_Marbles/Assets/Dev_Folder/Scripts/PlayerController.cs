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

    private void Awake()
    {
        marbleCount = GameObject.Find("Marble Count").GetComponent<Text>();
        winText = GameObject.Find("Win Text").GetComponent<Text>();
        healthRemain = GameObject.Find("Health Remain").GetComponent<Text>();
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

    void decrementHealth()
    {
           //get the current health
        int curr = int.Parse(healthRemain.text);
        //decrement the health by one
        curr--;
        healthRemain.text = curr.ToString();

        //check if health drops to 0 
        if(curr == 0)
        {
            winText.text = "You Lose!";
            //add some operation, possible back to the main menu
        }
    }

}
