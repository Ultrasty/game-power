﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerController : Enemy
{
    [SerializeField] public float speed;
    [SerializeField] public float searchRadius;
    [SerializeField] public float attackRadius;//攻击距离
    [SerializeField] public float attackInterval;//攻击间隔

    [SerializeField] private Transform playerTransform;
    [SerializeField] Animator myAnimator;
    [SerializeField] private bool finding;//是否找到玩家
    [SerializeField] private float myAttackTimer;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        myAnimator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        base.FixedUpdate();
        SearchPlayer();
        
        //myAttackTimer = Time.deltaTime;


    }

    void SearchPlayer()
    {
        if (playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            Debug.Log("Dist Vector:" + (transform.position - playerTransform.position).ToString());
            Debug.Log("distance:" + distance.ToString());
            
            if (distance <= attackRadius)
            {
                Attack();
            }
            else if(distance <= searchRadius)
            {
                Debug.Log("find you");
                myAnimator.SetBool("Tracking",true);
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
                finding = true;
            }
        }
    }

    void Attack()
    {
        /*
        if (myAttackTimer >= attackInterval)
        {
            Debug.Log("Attack");
            myAnimator.SetBool("Attack",true);
        }*/
        myAnimator.SetBool("Attack", true);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
