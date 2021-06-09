using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swingDamage : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    // {
    //     if (collider.IsTouching(playerFeetCollider))
    //     {
            
    //         HealthBar player = playerFeetCollider.GetComponent<HealthBar>();
    //         player.TakeDamage(damage);
    //     }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("撞到了2");
            HealthBar player = col.gameObject.GetComponent<HealthBar>();
            if (player != null)
            {
                Debug.Log("撞到了3");
                player.TakeDamage(damage);
            }
        }
    }
}
