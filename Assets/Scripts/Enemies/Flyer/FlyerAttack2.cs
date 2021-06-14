using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerAttack2 : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    private FlyerController flyer;
    private Collider2D col;
    private GameObject player;

    private void Start()
    {
        flyer = GameObject.Find("Flyer").GetComponent<FlyerController>();
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
            player.BroadcastMessage("TakeDamage", flyer.attackDamage);
        }
    }
}
