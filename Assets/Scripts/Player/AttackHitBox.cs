using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    Collider2D collider;
    GameObject player;
    public AudioSource block_source;
    public AudioSource attack_source;
    public bool is_play = false;
    private float track_length;
    public float remain;

    private void Start()
    {
      collider = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        remain = track_length;
        is_play = false;
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (player.GetComponent<FiniteStateMachine>().state == FiniteStateMachine.State.attacking)
        {
            if (collider.gameObject.tag == "Prop" || collider.gameObject.tag == "Enemy")
            {
                attack_source.Play();
                collider.gameObject.BroadcastMessage("TakeDamage");
            }
        }
        else if(player.GetComponent<FiniteStateMachine>().state == FiniteStateMachine.State.blocking)
        {
            if (collider.gameObject.tag == "Prop" || collider.gameObject.tag == "Enemy")
            {
                block_source.Play();
            }
            

        }
    }
}
