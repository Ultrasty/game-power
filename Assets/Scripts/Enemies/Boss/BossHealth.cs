using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

  public int health;
  public GameObject deathEffect;
  public AudioSource deathAudio;

  public void TakeDamage()
  {
    health -= 20;
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
    deathAudio.Play();
    Instantiate(deathEffect, transform.position, Quaternion.identity);
    Destroy(gameObject);
    //AudioSource.PlayClipAtPoint(deathAudio, transform.position);

  }

}
