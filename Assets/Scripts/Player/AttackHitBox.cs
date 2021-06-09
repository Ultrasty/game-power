using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    Collider2D collider;
    GameObject player;
    private AudioSource audioSource;
    public bool is_play = false;
    private float track_length;
    public float remain;

    private void Start()
    {
      collider = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = player.GetComponent<AudioSource>();
        track_length = audioSource.clip.length;
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
                collider.gameObject.BroadcastMessage("TakeDamage");
            }
        }
        else
        {
            if (collider.gameObject.tag == "Prop" || collider.gameObject.tag == "Enemy")
            {
                audioSource.Play();
            }
            

        }
    }
}
