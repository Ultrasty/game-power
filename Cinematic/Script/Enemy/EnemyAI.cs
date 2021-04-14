using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 400f;
    public float nextWayPointDistance = 3f;//距离一个路标点多近时移动向下一个
    public float radius;//检测半径

    Path path;//保存路径
    int currentWayPoint = 0;//正在向哪点移动
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //1秒寻路一次
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path P)
    {
        if (!P.error)
        {
            path = P;
            currentWayPoint = 0;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;

        float distanceToPlyaer = (transform.position - playerTransform.position).sqrMagnitude;//判断两点距离
        //到达终点结束
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;//得到现在前往的路径点设置和当前位置的距离的向量
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
        if (distance < nextWayPointDistance && distanceToPlyaer < radius)
        {
            currentWayPoint++;
        }

        //转向
        if (rb.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }
}
