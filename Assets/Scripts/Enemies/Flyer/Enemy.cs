using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public float flashTime;

    private SpriteRenderer sr;
    private Color originalColor;
    //private PlayerHealth playerHealth;
    // Start is called before the first frame update
    public void Start()
    {
        //获取player对象下的playerHealth组件
        //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        FlashColor(flashTime);
        //Instantiate(bloodEffect, transform.position, Quaternion.identity);
        //GameController.camShake.Shake();
    }

    //受伤红光闪烁时间time
    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);//延迟调用重置颜色
    }

    void ResetColor()
    {
        sr.color = originalColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //检测是否碰撞到玩家
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString()== "UnityEngine.CapsuleCollider2D")
        {
            
        }
    }
}
