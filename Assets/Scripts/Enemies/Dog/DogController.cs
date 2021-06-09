﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DogController : MonoBehaviour
{
  
  [SerializeField] private float visionRange = 15f;
  [SerializeField] private float verticalVisionRange = 2.5f;
  [SerializeField] private float attackRange = 5f;
  [SerializeField] private float HealthPoints = 2f;
  [SerializeField] public float attackDamage = 5f;
  [SerializeField] public float runSpeed = 5f;
  [SerializeField] public bool destroyable = true;
  
  [SerializeField] private float rayLenght;
  [SerializeField] private Vector3 colliderOffset;
  [SerializeField] private LayerMask groundLayer;
  [SerializeField] private Transform player;
  
  private bool onGround;
  private float distanceToPlayer;
  private float verticalDistanceToPlayer;
  private bool isFlipped = true;
  private Animator anim;
  private Rigidbody2D rb;
  private Vector2 target;
  public GameObject collectObject;

    private void Start()
  {
    anim = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, rayLenght, groundLayer)
            || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, rayLenght, groundLayer);
            
    target = new Vector2(player.position.x, rb.position.y);    
    distanceToPlayer = Vector2.Distance(rb.position, target);
    
    verticalDistanceToPlayer = Mathf.Abs(rb.position.y - player.position.y);

    ChooseAnimation();
  }
  
  private void ChooseAnimation()
  {
    // if(verticalDistanceToPlayer > verticalVisionRange)
    // {
    //   anim.SetBool("InVisionRange", false);
    // }
    
    if(distanceToPlayer < visionRange)
    {
      anim.SetBool("InVisionRange", true);
      if(distanceToPlayer < attackRange)
      {
        anim.SetTrigger("Attack");
      }
    } 
    else
    {
      anim.SetBool("InVisionRange", false);
    }
  }
  
  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * rayLenght);
    Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * rayLenght);
    Gizmos.color = Color.green;
    Gizmos.DrawLine(transform.position, transform.position + Vector3.right * visionRange * transform.localScale.z);
    Gizmos.color = Color.blue;
    Gizmos.DrawLine(transform.position, transform.position + Vector3.right * attackRange * transform.localScale.z);
  }
  
  public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}
  
  private void TakeDamage()
  {
    anim.SetTrigger("Hurt");
    
    if(destroyable)
    {
      HealthPoints -= 1;
    }
      
    if(HealthPoints <= 0)
    {
      Die();
    }
  }
  
  private void Die()
  {
    anim.SetBool("IsDead", true);
    Instantiate(collectObject, transform.position, Quaternion.identity);
    GetComponent<Collider2D>().enabled = false;
    rb.gravityScale = 0;
    rb.velocity = new Vector2(0,0);
    this.enabled = false;
    Destroy(this);
  }
    /*
    //随机掉落子弹或者草莓
    public void RandomDrop()
    {
        //草莓与子弹掉落概率为1：2。12~19生成草莓，20~31生成子弹，
        int num = (int)(Random.Range(12, 31) / 10);
        if (num % 2 == 0&& num % 2 == 1)
        {
            Debug.Log("生成樱桃");
            collectObject = Instantiate(Resources.Load("/Assets/Prefab/Enviroment/Props/Cherry"), gameObject.transform.position, Quaternion.identity) as GameObject;
        }
        
        if (num % 2 == 1)
        {
            collectObject = Instantiate(Resources.Load("Prefab/CollectibleHealth"), gameObject.transform.position, Quaternion.identity) as GameObject;
        }
    }*/
}
