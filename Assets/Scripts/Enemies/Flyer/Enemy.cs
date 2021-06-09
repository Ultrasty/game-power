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
        float faceDir = Input.GetAxisRaw("Horizontal");
        if (faceDir != 0)
        {
             transform.localScale = new Vector3(faceDir,1,1);
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
