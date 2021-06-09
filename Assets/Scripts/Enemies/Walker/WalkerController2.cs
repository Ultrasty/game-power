using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerController2 : MonoBehaviour
{

    public float speed;//巡逻速度
    public float patrolRadius;//巡逻半径
    public float trackSpeed;//追踪速度
    public float searchRadius;//搜索半径
    public float loseRadius;//丢失目标半径
    public float attackRadius;//攻击半径


    Vector2 myPatrolStart;//巡逻起点
    Vector2 myPatrolEnd;//巡逻终点
    float myPatrolDuration;//巡逻时长

    Vector2 myMoveVelocity;//当前移动速度

    Animator myAnimator;
    Rigidbody2D myRigidbody2D;
    SpriteRenderer MySpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        //获取组件
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        MySpriteRenderer = GetComponent<SpriteRenderer>();

        //计算巡逻区域
        myPatrolStart = transform.position + Vector3.left * patrolRadius;
        myPatrolEnd = transform.position + Vector3.right * patrolRadius;
        //计算巡逻时长
        myPatrolDuration = (myPatrolStart - myPatrolEnd).magnitude / speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	myMoveVelocity.y = myRigidbody2D.velocity.y;//获取刚体y值
        myRigidbody2D.velocity = myMoveVelocity;//重新赋值
        myMoveVelocity.x = 0.0f;
        Patrol();
    }

    //巡逻
    void Patrol()
    {
        float t = Mathf.InverseLerp(0, myPatrolDuration, Mathf.PingPong(Time.time, myPatrolDuration));//反向差值得到时间点
        Vector2 positoin = Vector2.Lerp(myPatrolStart, myPatrolEnd, t);//得到时间点对应位置
        myMoveVelocity.x = (positoin - (Vector2)transform.position).normalized.x * speed;
    }
}
