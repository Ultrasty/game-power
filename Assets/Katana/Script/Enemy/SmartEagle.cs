using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEagle : Enemy
{
    public float speed = 5f;
    public float radius;//检测半径

    private Rigidbody2D EagleRigidbody;
    private Transform playerTransform;
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
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
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            }
        }
    }

    void Flip()//翻转
    {
        bool hasXAxisSpeed = Mathf.Abs(EagleRigidbody.velocity.x) > Mathf.Epsilon;//Epsilon极小值
        if (hasXAxisSpeed)
        {
            if (EagleRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (EagleRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
