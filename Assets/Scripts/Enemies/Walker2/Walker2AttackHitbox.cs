using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker2AttackHitbox : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    private Walker2Controller walker2;
    private Collider2D col;
    private GameObject player;

    private void Start()
    {
        walker2 = GameObject.Find("Walker2").GetComponent<Walker2Controller>();
        col = GetComponent<Collider2D>();
    }
    /*
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.name == "Player")
        {
            // get player's health bar script and TakeDamage()
            HealthBar player = col.gameObject.GetComponent<HealthBar>();
            player.TakeDamage(flyer.attackDamage);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.name == "Player")
        {
            // get player's health bar script and TakeDamage()
            player = GameObject.Find("Player");
            Debug.Log("start to attack");
            player.BroadcastMessage("TakeDamage", walker2.attackDamage);
        }
    }
}
