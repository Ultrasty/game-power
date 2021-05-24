using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEagle : Enemy
{
    public float speed = 5f;
    public float radius;//检测半径

    Rigidbody2D enemyRb;
    private Transform playerTransform;
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        enemyRb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();
        if(playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;//判断两点距离

            if(distance < radius)
            {
                Debug.Log("start tracing");
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            }
        }

        //转向
        if (enemyRb.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (enemyRb.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
