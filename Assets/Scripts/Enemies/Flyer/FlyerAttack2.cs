using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerAttack2 : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    private FlyerController flyer;
    private PolygonCollider2D col;
    private GameObject player;

    private void Start()
    {
        flyer = GameObject.Find("Flyer").GetComponent<FlyerController>();
        col = GetComponent<PolygonCollider2D>();
    }

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
