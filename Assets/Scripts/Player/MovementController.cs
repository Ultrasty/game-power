﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{  
  [Header("Horizontal Movement")]
  [SerializeField] public float walkSpeed = 5f;
  [SerializeField] public float runSpeed = 5f;
  [SerializeField] private float runStaminaCost = 1f;
  [SerializeField] private ParticleSystem dust;

  private StaminaBar staminaBar;
  private Rigidbody2D rb;
  private CollisionRay ray;
  private Vector2 direction;
  private FiniteStateMachine fsm;
  private bool facingRight = true;
  private bool onGround;
    private bool bulletTime = false;

  private void Start()
  {
    staminaBar = GetComponent<StaminaBar>();
    rb = GetComponent<Rigidbody2D>();
    ray = GetComponent<CollisionRay>();    
    fsm = GetComponent<FiniteStateMachine>();
  }

  private void Update()
  {
    onGround = ray.OnGround();
    direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
  }
  private void FixedUpdate()
  {
    changeSpeed();
    if(direction.x != 0 && fsm.state != FiniteStateMachine.State.rolling  && fsm.state != FiniteStateMachine.State.attacking)
    {
      Move();
    }
  }
  
  private void Move()
  {
    bool clicado;
    
    // Running
    if(Input.GetKey(KeyCode.LeftShift))
    {
      clicado = true;
      {
        if(direction.x != 0 && onGround)
        {
          if(staminaBar.EnoughStamina() && clicado)
          {
            dust.Play();
            staminaBar.UseStamina(runStaminaCost);
            rb.velocity = new Vector2(direction.x * runSpeed, rb.velocity.y);
          }
          else
          {
            clicado = false;
          }
        }
      }
    }
    // Walking
    else 
    {
      rb.velocity = new Vector2(direction.x * walkSpeed, rb.velocity.y);
    }

    if((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
    {
      Flip();
    }
  }
  
  private void Flip()
  {
    // Invert the player localScale transform in the X-axis
    facingRight = !facingRight;
    transform.localScale = new Vector2(Mathf.Sign(direction.x), transform.localScale.y);
  }

  private void changeSpeed()
    {
        GameObject self_ob = GameObject.FindGameObjectWithTag("Player");
        if(self_ob.GetComponent<CharacterController2D>().bulletTime&& (!bulletTime))
        {
            bulletTime = true;
            walkSpeed *= 3;
            runSpeed *= 3;
        }
        else if (!self_ob.GetComponent<CharacterController2D>().bulletTime && (bulletTime))
        {
            bulletTime = false;
            walkSpeed /= 3;
            runSpeed /= 3;
        }
    }
}
