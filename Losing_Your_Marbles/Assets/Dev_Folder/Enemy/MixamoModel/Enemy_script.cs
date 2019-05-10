﻿using System.Collections;
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
    PlayerController player;
    private bool moved;
    private bool attacked;
    private Vector3 avoidBuffer;

    // Start is called before the first frame update
    void Start()
    {
        enemAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        playPos = GameObject.Find("Player").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        avoidBuffer = new Vector3(Random.Range(-1.2f, 1.2f), 0, Random.Range(-1.2f, 1.2f));
        moved = false;
        attacked = false;
    }

    // Update is called once per frame
    void Update()
    {

        float distFromPlayer = Vector3.Distance(transform.position, playPos.position);
        if (!player.Visible()) distFromPlayer = int.MaxValue;
        if (followMinDist < distFromPlayer && distFromPlayer < followMaxDist) 
        {
            nav.isStopped = false;
            nav.SetDestination(playPos.position+avoidBuffer);
            enemAnim.SetBool("isWalking", true);
            moved = true;
        } 
        else 
        {
            nav.isStopped = true;
            enemAnim.SetBool("isWalking", false);
        }

        if(player.Visible() && !attacked && (Vector3.Distance(transform.position, playPos.position) <= attackDist))
        {
            moved = false;
            attacked = true;
            StartCoroutine(attack());
            StartCoroutine(attackWait());
        }
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
}
