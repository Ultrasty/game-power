﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
  [Header("Combo Parameters")]
  // how much room until next click to continue combo
  [Range(0f, 2f)] [SerializeField] private float maxComboDelay = 0.5f;
  [Range(0f, 0.5f)] [SerializeField] private float maxBlockDelay = 0.1f;
  [SerializeField] private float lastClickedTime = 0;
  [SerializeField] private float lastBlockedTime = 0;
  [SerializeField] private int numberOfClicks = 0;

  [Header("Weak Attack Parameters")]
  [Range(1f, 120f)] [SerializeField] private float motionPerAttack = 100f;
  [Range(0f, 100f)] [SerializeField] private float WeakAttackStaminaCost = 0;
  [SerializeField] private Collider2D AttackHitBox;
  [SerializeField] private LayerMask hittableLayers;

  // [Header("Components")]
  public bool is_block = false;
  public bool is_attack = false;
  private FiniteStateMachine fsm;
  private StaminaBar staminaBar;
  private Rigidbody2D rb;
  private Animator anim;



  private void Start()
  {
    fsm = GetComponent<FiniteStateMachine>();
    staminaBar = GetComponent<StaminaBar>();
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
  }


  private void Update()
  {
    if (!is_block)
    // Checks if the next click is within the chain time, which will proceed the combo
    {
      if (Time.time - lastClickedTime > maxComboDelay)
      {
        numberOfClicks = 0;
      }
      if (!is_attack)
      {
        if (Input.GetMouseButtonDown(1))
        {

          is_block = true;
          numberOfClicks = 0;
          Block();
        }
      }
      else
      {
        if (Time.time - lastClickedTime > maxBlockDelay)
        {
          is_attack = false;
        }
      }
      // key listener to mouse1/fireButton/MouseLeftClick as you wanna call it
      if (Input.GetMouseButtonDown(0))
      {
        // Call the combo manager which will identify what animation will be played
        is_attack = true;
        Combo();
      }
    }
    else
    {
      if (Time.time - lastBlockedTime > maxBlockDelay)
      {
        is_block = false;
      }
    }
  }
  private void Block()
  {
    if (staminaBar.EnoughStamina())
    {
      rb.velocity = new Vector2(0, 0);
      fsm.state = FiniteStateMachine.State.blocking;
      anim.SetInteger("state", 10);
      //audioSource.Play();
      lastBlockedTime = Time.time; // get current time // increase clicks counter
    }

    else
    {
      is_block = false;
    }
  }
  // responsible for the stamina verification and consuption
  private int staminaUse(float amount)
  {
    if (staminaBar.EnoughStamina())
    {
      staminaBar.UseStamina(amount);
      return 0;
    }
    return 1;
  }

  // responsible for moving the character throughout the attack animation
  public void MoveCharacter()
  {
    staminaBar.UseStamina(WeakAttackStaminaCost);
    rb.position = new Vector2(rb.position.x + Mathf.Sign(transform.localScale.x) * motionPerAttack, rb.position.y);
    // rb.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * motionPerAttack, rb.velocity.y);
    // Debug.Log(Mathf.Sign(transform.localScale.x) * motionPerAttack);
  }

  // Used to control if next and/or which animation will be played
  private void Combo()
  {
    if (staminaBar.EnoughStamina())
    {
      rb.velocity = new Vector2(0, 0);
      fsm.state = FiniteStateMachine.State.attacking;
      lastClickedTime = Time.time; // get current time
      numberOfClicks++; // increase clicks counter
      ComboVerification();
    }
    else
    {
      is_attack = false;
      numberOfClicks = 0;
    }
  }

  private void ComboVerification()
  {
    numberOfClicks = 2 - (numberOfClicks % 2);

    switch (numberOfClicks)
    {
      case 0:
        anim.SetBool("Attack1", false);
        anim.SetBool("Attack2", false);
        numberOfClicks = 0;
        return;
      case 1:
        anim.SetBool("Attack1", true);
        return;

      case 2:
        anim.SetBool("Attack2", true);
        return;

      default:
        return;
    }
  }

  // Used as a flag on Weak_Attack_OnGround_1 animation
  // detects if another click was given
  // if yes proceed to the next animation otherwise return to idle
  public void Return1()
  {
    if (numberOfClicks == 2)
    {
      anim.SetBool("Attack1", true);
      anim.SetBool("Attack2", true);
    }
    else
    {
      anim.SetBool("Attack1", false);
      numberOfClicks = 0;
      fsm.state = FiniteStateMachine.State.emptyState;
    }
  }

  // Used as a flag on Weak_Attack_OnGround_2 animation
  public void Return2()
  {
    if (numberOfClicks == 1)
    {
      anim.SetBool("Attack1", true);
      anim.SetBool("Attack2", false);
    }
    else
    {
      anim.SetBool("Attack1", false);
      anim.SetBool("Attack2", false);
      numberOfClicks = 0;
      fsm.state = FiniteStateMachine.State.emptyState;
    }
  }
}
