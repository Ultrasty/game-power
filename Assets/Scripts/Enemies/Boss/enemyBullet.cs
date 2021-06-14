using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
  public float speed = 20f;
  public float damage;
  public Rigidbody2D rb;
  public GameObject impactEffect;

  // Use this for initialization
  void Start()
  {
    rb.velocity = -transform.right * speed;
  }

  void OnTriggerEnter2D(Collider2D hitInfo)
  {
    Debug.Log("OnTriggerEnter:" + hitInfo.transform.name);

    if (hitInfo.transform.name == "Player")
    {
      Debug.Log("打到玩家了");
      //   hitInfo.GetComponent<HealthBar>().TakeDamage(damage);
      Instantiate(impactEffect, transform.position, transform.rotation);
      Destroy(gameObject);
      HealthBar player = hitInfo.GetComponent<HealthBar>();
      player.TakeDamage(damage);
    }


  }

}
