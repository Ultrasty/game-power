﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
  [SerializeField] private Slider healthBar;
  [SerializeField] private int maxHealth = 100;
  
  // when healing
  [Range(0f, 1f)] [SerializeField] private float regenDelay;
  [Range(0f, 10f)] [SerializeField] private float regenAmount;
  [SerializeField] private float HealthPotion;

  // micro delay no recharge rate
  private WaitForSeconds regenTick;
  private float currentHealth;
    Scene scene;
    // processo de regeneracao
    private Coroutine regen;
    private GameObject player;
    Vector2 start_point;
  
  private void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
        scene = SceneManager.GetActiveScene();
        start_point = player.transform.position;
    regenTick = new WaitForSeconds(regenDelay);
    currentHealth = maxHealth;      // inicializa a barra com a stamina cheia
    healthBar.maxValue = maxHealth; // configura o valor maximo da barra
    healthBar.value = maxHealth;    // atualiza o preenchimento da barra
        
    }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.G)){
      // Condicional necessaria para que exista o delay apos o uso da stamina
      if(regen != null) // se a regeneracao ja foi inicializada
      {
          // encerra o processo de regeneracao
          StopCoroutine(regen);
      }
      // Chama o metodo de regeneracao
      regen = StartCoroutine(this.Heal(HealthPotion));
    }
      
    if (Input.GetKeyDown(KeyCode.H)){
      this.TakeDamage(20f);
    }

  }

  public void TakeDamage(float amount)
  {
      // Verifica se tem vida suficiente para realizar a acao
      if(player.GetComponent<FiniteStateMachine>().state == FiniteStateMachine.State.blocking)
        {
            
            return;
        }
      
      if(currentHealth > 0)
      {
        currentHealth -= amount; // reduz a qtd usada da vida atual
        healthBar.value = currentHealth; 
        if(currentHealth<=0)
            {
                dead();
            }
            // atualiza o valor na barra
      }
  }

  private IEnumerator Heal(float amount)
  {
    float targetHP = currentHealth + amount;
    if(targetHP > maxHealth) 
    {
      targetHP = maxHealth;
    }
    
    while(currentHealth < targetHP)
    {
      currentHealth += regenAmount;
      healthBar.value = currentHealth;
      // micro delay no recharge
      yield return regenTick;
    }
    regen = null;
  }

  private void dead()
  {
        //player.transform.position = start_point;
        SceneManager.LoadScene(scene.name);
  }
}
