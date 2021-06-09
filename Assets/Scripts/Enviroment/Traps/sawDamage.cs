using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawDamage : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {

        // if (collider.IsTouching(playerFeetCollider))
        // {
            
        //     HealthBar player = playerFeetCollider.GetComponent<HealthBar>();
        //     player.TakeDamage(damage);
        // }

    }
    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        HealthBar player = hitInfo.gameObject.GetComponent<HealthBar>();
        
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
