using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerAttack : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    private WalkerController2 walker;
    private PolygonCollider2D col;
    private GameObject player;

    private void Start()
    {
        walker = GameObject.Find("Flyer").GetComponent<WalkerController2>();
        col = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.name == "Player")
        {
            // get player's health bar script and TakeDamage()
            player = GameObject.Find("Player");
            player.BroadcastMessage("TakeDamage", walker.attackDamage);
        }
    }
}
