using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float speed;//巡逻速度
    public float trackSpeed;//追踪速度
    public float patrolRadius;//巡逻半径
    public float searchRadius;//搜索半径
    public float loseRadius;//丢失目标半径
    public float attackRadius;//攻击半径
    public float attackInterval;//攻击间隔
    public LayerMask searchLayer;

    public Damager attackDamager;

    Transform myTarget=null;

    float myAttackTimer;
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
        if (myTarget)
        {
            Track();
        }
        else
        {
            Patrol();
            Search();
        }
        myMoveVelocity.y = myRigidbody2D.velocity.y;//获取刚体y值
        myRigidbody2D.velocity = myMoveVelocity;//重新赋值
        myMoveVelocity.x = 0.0f;

        myAttackTimer = Time.deltaTime;
    }

    //巡逻
    void Patrol()
    {
        float t = Mathf.InverseLerp(0, myPatrolDuration, Mathf.PingPong(Time.time, myPatrolDuration));//反向差值得到时间点
        Vector2 positoin = Vector2.Lerp(myPatrolStart, myPatrolEnd, t);//得到时间点对应位置
        myMoveVelocity.x = (positoin - (Vector2)transform.position).normalized.x * speed;
    }

    void Search()
    {
        //搜索圆形半径内玩家
        Collider2D collider = Physics2D.OverlapCircle(transform.position, searchRadius, searchLayer);
        if (collider)//如果检测到
        {
            //Debug.Log("find you!");
            myTarget = collider.transform;
        }
    }

    void Track()
    {
        //检测与目标的距离
        float sqrDistance = Vector3.Distance(transform.position , myTarget.position);
        //Debug.Log("zombie distance" + sqrDistance.ToString());
        //检测是否在攻击范围内
        if(sqrDistance <= attackRadius)
        {
            Attack();
        }else if(sqrDistance < loseRadius)//在追击范围内
        {
            //Debug.Log("tracking you!");
            myMoveVelocity.x = (myTarget.position - transform.position).normalized.x * trackSpeed;
        }
        else//超出追击范围
        {
            myTarget = null;
        }

    }

    void Attack()
    {
        if(myAttackTimer >= attackInterval)
        {
            myAnimator.SetTrigger("Attack");
            attackDamager.TriggerDamageOnce(); 
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, loseRadius);
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
