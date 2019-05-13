﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject FreezeGun;
    public GameObject OpenCH;
    public GameObject ClosedCH;
    public Text marbleCount;
    public Text winText;
    public Text itemUsage;
    public Text healthRemain;
    public Slider remainingHealthSlider;
    public Transform trap;
    public Transform miniTrap;
    public AudioSource shootAudio;
    public AudioSource freezegunAudio;
    public AudioSource trapAudio;
    public AudioSource healthAudio;
    public AudioSource invisAudio;
    public AudioSource speedAudio;
    public AudioSource damageAudio;
    public AudioSource gatherAudio;
    public AudioSource pickupAudio;
    public Image damagePanel;
    public GameObject pausemenu;
    private bool paused;

    private bool isVisible;

    int selected;
   
    private void Awake()
    {
        damagePanel.enabled = false;
        marbleCount = GameObject.Find("Marble Count").GetComponent<Text>();
        winText = GameObject.Find("Win Text").GetComponent<Text>();
        itemUsage = GameObject.Find("Item Usage").GetComponent<Text>();
        healthRemain = GameObject.Find("Health Remain").GetComponent<Text>();
        selected = 1;
        GameObject.Find("Ammo Title").GetComponent<Text>().color = new Color(255, 215, 0);
        this.isVisible = true;
        this.paused = false;
        
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

        if (paused)
        {
            if(Input.GetKeyDown(KeyCode.P)){
                Time.timeScale = 1;
                this.paused = !paused;
                this.pausemenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Time.timeScale = 0;
                this.paused = !paused;
                this.pausemenu.SetActive(true);
            }

        }

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
                    StartCoroutine(StartAudio(gatherAudio));
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
        if (Input.GetMouseButtonDown(0))
        {
            switch (selected)
            {
                case 2:
                    Text trapCt = GameObject.Find("Traps Count").GetComponent<Text>();
                    if (int.Parse(trapCt.text) > 0)
                    {
                        Instantiate(this.trap, this.transform.position - new Vector3(0, 0.25f, 0), Quaternion.identity);
                        Instantiate(this.miniTrap, GameObject.Find("MiniMapPlayer").GetComponent<Transform>().position, GameObject.Find("MiniMapPlayer").GetComponent<Transform>().rotation);
                        trapCt.text = (int.Parse(trapCt.text) - 1).ToString();
                        StartCoroutine(StartAudio(trapAudio));
                    }
                    break;
                case 3:
                    Text invCt = GameObject.Find("Invisibility Count").GetComponent<Text>();
                    if (int.Parse(invCt.text) > 0)
                    {
                        this.isVisible = false;
                        invCt.text = (int.Parse(invCt.text) - 1).ToString();
                        itemUsage.text = "Invisibility On";
                        StartCoroutine("VisibleAfterTime");
                        StartCoroutine(StartAudio(invisAudio));
                    }
                    break;
                case 4:
                    Text speedCt = GameObject.Find("Speed Boost Count").GetComponent<Text>();
                    if (int.Parse(speedCt.text) > 0)
                    {

                        UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController c  = GameObject.Find("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
                        c.movementSettings.RunMultiplier = 5;
                        speedCt.text = (int.Parse(speedCt.text) - 1).ToString();
                        itemUsage.text = "Speed Boost On";
                        StartCoroutine("SlowAfterTime");
                        StartCoroutine(StartAudio(speedAudio));

                    }
                    break;
                case 6:
                    Text hpCt = GameObject.Find("Health Potion Count").GetComponent<Text>();
                    if (int.Parse(hpCt.text) > 0)
                    {
                        healthRemain.text = 5.ToString();
                        remainingHealthSlider.value = 5;
                        hpCt.text = (int.Parse(hpCt.text) - 1).ToString();
                        StartCoroutine(StartAudio(healthAudio));
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
            StopCoroutine(StartAudio(pickupAudio));
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
                count.text = (int.Parse(count.text) + Random.Range(15,35)).ToString();
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
                count.text = (int.Parse(count.text) + Random.Range(15, 35)).ToString();
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
            remainingHealthSlider.value = remainingHealthSlider.value - 1;
            StartCoroutine(StartAudio(damageAudio));
            damagePanel.enabled = true;
            StartCoroutine(DamagePanelController());

        }

        //check if health drops to 0 
        if(curr == 0)
        {
            winText.text = "You Lose!";
            //add some operation, possible back to the main menu
        }
    }

    IEnumerator DamagePanelController()
    {
        yield return new WaitForSeconds(1);
        damagePanel.enabled = false;

    }

    IEnumerator SlowAfterTime()
    {
        yield return new WaitForSeconds(10);
        UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController c = GameObject.Find("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
        c.movementSettings.RunMultiplier = 2;
        itemUsage.text = "";
    }

    IEnumerator VisibleAfterTime()
    {
        yield return new WaitForSeconds(30);
        this.isVisible = true;
        itemUsage.text = "";
    }

    public void setVisibility(bool v) {
        this.isVisible = v;
    }

    public bool Visible() {
        return this.isVisible;
     }

    //Returns 0 for no gun, 1 for normal, 2 for freeze
    public int gunChoice()
    {
        if(selected == 1)
        {
            return 1;
        } 
        else if (selected == 5)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }

    public bool hasAmmo()
    {
        bool result;
        if (selected == 1)
        {
            Text ammoCt = GameObject.Find("Ammo Count").GetComponent<Text>();
            if (int.Parse(ammoCt.text) > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
        else if (selected == 5)
        {
            Text freezeCt = GameObject.Find("Freeze Gun Count").GetComponent<Text>();
            if (int.Parse(freezeCt.text) > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
        else
        {
            result = false;
        }

        return result;
    }

    public void shot()
    {
        if (selected == 1)
        {
            Text ammoCt = GameObject.Find("Ammo Count").GetComponent<Text>();
            ammoCt.text = (int.Parse(ammoCt.text) - 1).ToString();
            StartCoroutine(StartAudio(shootAudio));
           
        }
        else if (selected == 5)
        {
            Text freezeCt = GameObject.Find("Freeze Gun Count").GetComponent<Text>();
            freezeCt.text = (int.Parse(freezeCt.text) - 1).ToString();
            StartCoroutine(StartAudio(freezegunAudio));
        }
    }


    IEnumerator StartAudio(AudioSource source)
    {
        source.Play();
        yield return new WaitForSeconds(source.clip.length);

    }

}
