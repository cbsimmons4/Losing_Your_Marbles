using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float fireRate;
    public float range;
    public PlayerController playerChoice;
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem flash;
    LineRenderer gunLine;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        playerChoice = GameObject.Find("Player").GetComponent<PlayerController>();
        flash = GameObject.Find("barrel_shot").GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;


        if (Input.GetMouseButton(0) && timer >= fireRate && playerChoice.hasAmmo())
        {
            if (Time.timeScale == 1) { shoot(); }

        }
       
        if (timer >= fireRate * 0.2f)
        {
            // ... disable the effects.
            gunLine.enabled = false;
        }
    }

    IEnumerator StartAudio(AudioSource source)
    {

        source.Play();
        yield return new WaitForSeconds(source.clip.length);

    }


    void shoot()
    {
        timer = 0f;

        flash.Stop();
        //create because csharp won't allow a new color on one line
        var main = flash.main;
        if (playerChoice.gunChoice() == 1)
        {
            main.startColor = new Color(255, 91, 0, 255);
        }
        else
        {
            main.startColor = new Color(0, 210, 255, 255);
        }
        flash.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        playerChoice.shot();

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            if (shootHit.collider.CompareTag("enemy"))
            {
                Enemy_script enemy = shootHit.collider.GetComponent<Enemy_script>();

                if (enemy != null)
                {
                    if (playerChoice.gunChoice() == 1 && playerChoice.hasAmmo())
                    {
                        enemy.death();
                    }
                    else if (playerChoice.gunChoice() == 2 && playerChoice.hasAmmo())
                    {
                        enemy.freeze();
                    }
                }
            }
            if (shootHit.collider.CompareTag("marble")) { 
                if(playerChoice.gunChoice() == 2 && playerChoice.hasAmmo())
                {
                    shootHit.collider.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                }

            }
        }
        else
        {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
