using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float speed = 1;          // The speed our bullet travels
    public Vector2 targetVector;    // the direction it travels
    public float lifetime = 10f;     // how long it lives before destroying itself
    public float damage = 10;       // how much damage this projectile causes

    void Start()
    {
        // find our RigidBody
        Rigidbody2D rb = gameObject.GetComponentInChildren<Rigidbody2D>();
        if (Time.timeScale != 1f)
            speed = speed * 10 / 6;
        // add force 
        //rb.AddForce(targetVector.normalized * speed);
        rb.velocity = targetVector.normalized * speed;
    }


    // Update is called once per frame
    void Update()
    {
        // decrease our life timer
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            // we have ran out of life
            Destroy(this.gameObject);    // kill me
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        //Physics2D.IgnoreCollision(collision2D.collider, GetComponent<Collider2D>());
        if (collision2D.collider.tag == "Enemy" || collision2D.collider.tag == "Prop")
        {
            Debug.Log("go");
            Destroy(gameObject); 
        }
        GameObject col_obj = collision2D.collider.gameObject;
        Rigidbody2D may;
        if (col_obj.TryGetComponent<Rigidbody2D>(out may))
        {
            Vector2 col_v = col_obj.GetComponent<Rigidbody2D>().velocity;
            if (col_v.x != 0 || col_v.y != 0)
            {
                col_v.x = 0;
                col_v.y = 0;
                collision2D.collider.gameObject.GetComponent<Rigidbody2D>().velocity = col_v;
            }
        }

        if (collision2D.collider.gameObject.tag == "Prop" || collision2D.collider.gameObject.tag == "Enemy")
        {
            collision2D.collider.gameObject.BroadcastMessage("TakeDamage");
        }
    }
}
