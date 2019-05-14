using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_script : MonoBehaviour
{
    Animator enemAnim;
    Rigidbody rb;
    NavMeshAgent nav;
    Transform playPos;
    public float followMinDist;
    public float followMaxDist;
    public float attackDist;
    public float attackTime;
    public float freezeTime;
    PlayerController player;
    private bool moved;
    private bool attacked;
    private Vector3 avoidBuffer;
    private bool dead;
    private float nextUpdate;
    private float updateRate;
    private Vector3 lastPostion;
  
    // Start is called before the first frame update
    void Start()
    {
        updateRate = 1;
        nextUpdate = Time.time;
        lastPostion = transform.position;
        enemAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        playPos = GameObject.Find("Player").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        avoidBuffer = new Vector3(Random.Range(-1.2f, 1.2f), 0, Random.Range(-1.2f, 1.2f));
        moved = false;
        attacked = false;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
          if (!dead)
          {
              float distFromPlayer = Vector3.Distance(transform.position, playPos.position);
              if (!player.Visible()) distFromPlayer = int.MaxValue;
              if (followMinDist < distFromPlayer && distFromPlayer < followMaxDist)
              {

                if (Time.time >= nextUpdate)
                {
                    nextUpdate = Time.time + updateRate;
                    nav.isStopped = false;
                    nav.SetDestination(playPos.position + avoidBuffer);
                    if (Vector3.Distance(this.lastPostion, this.transform.position) < .04f)
                    {
                        enemAnim.SetBool("isWalking", false);
                    }
                    else {
                        enemAnim.SetBool("isWalking", true);
                    }
                    moved = true;
                }
               
              }
              else
              {
                  nav.isStopped = true;
                  enemAnim.SetBool("isWalking", false);
              }

              if (player.Visible() && !attacked && (Vector3.Distance(transform.position, playPos.position) <= attackDist))
              {
                  moved = false;
                  attacked = true;
                  StartCoroutine(attack());
                  StartCoroutine(attackWait());
              }

          }
        lastPostion = this.transform.position;
    }

    IEnumerator attack()
    {
        enemAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(attackTime);
        if (!moved)
        {
            player.decrementHealth();
        }
    }

    IEnumerator attackWait()
    { 
        yield return new WaitForSeconds(2.3f);
        attacked = false;
    }

    public void death()
    {
        dead = true;
        enemAnim.SetTrigger("Dead");
        nav.enabled = false;
        Destroy(gameObject, 2);
       
    }

    public void freeze()
    {
        nav.enabled = false;
        dead = true;
        enemAnim.SetBool("isWalking", false);
        StartCoroutine(freezeTimer());
    }

    IEnumerator freezeTimer()
    {
        yield return new WaitForSeconds(freezeTime);
        nav.enabled = true;
        dead = false;
    }
}
