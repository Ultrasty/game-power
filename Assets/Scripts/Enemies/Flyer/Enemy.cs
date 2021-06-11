using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int HealthPoints;
    public int damage;
    public float flashTime;

    private SpriteRenderer sr;
    private Color originalColor;
    private Rigidbody2D rb;
    //private PlayerHealth playerHealth;
    // Start is called before the first frame update
    public void Start()
    {
        //获取player对象下的playerHealth组件
        //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        rb = GetComponent<Rigidbody2D>();   //获取刚体组件
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        Flip();
    }

    void Flip()
    {
        bool HasXAxisSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if(HasXAxisSpeed)
        {
            if(rb.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (rb.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
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
}
