using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

  public int health = 500;

  public GameObject deathEffect;

  public bool isInvulnerable = false;

  public void TakeDamage()
  {
    if (isInvulnerable)
      return;

    health -= 20;
    Debug.Log(health);
    if (health <= 100)
    {
      GetComponent<Animator>().SetBool("IsEnraged", true);
    }

    if (health <= 0)
    {
      Die();
    }
  }

  void Die()
  {
    Instantiate(deathEffect, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }

}
