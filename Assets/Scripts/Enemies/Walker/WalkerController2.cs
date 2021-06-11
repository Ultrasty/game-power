using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerController2 : MonoBehaviour
{

    [SerializeField] public int HealthPoints;//生命值
    [SerializeField] public float speed;//巡逻速度
    [SerializeField] public float patrolRadius;//巡逻半径
    [SerializeField] public float trackSpeed;//追踪速度
    [SerializeField] public float searchRadius;//搜索半径
    [SerializeField] public float loseRadius;//丢失目标半径
    [SerializeField] public float attackRadius;//攻击半径
    [SerializeField] public LayerMask searchLayer;
    [SerializeField] public bool destroyable = true;
    [SerializeField] public float attackDamage = 1.0f;
    public GameObject collectObject;


    Vector2 myPatrolStart;//巡逻起点
    Vector2 myPatrolEnd;//巡逻终点
    float myPatrolDuration;//巡逻时长

    Vector2 myMoveVelocity;//当前移动速度

    Animator myAnimator;
    Rigidbody2D myRigidbody2D;
    SpriteRenderer MySpriteRenderer;
    [SerializeField] private Transform playerTransform;//玩家transform
    private float distance;//与玩家距离
    private bool movingRight = true;


    Transform myTarget;//是否有目标
    public float attackInterval;//攻击间隔
    float myAttackTimer;

    // Start is called before the first frame update
    void Start()
    {
        //获取组件
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        MySpriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

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
        //Debug.Log("distance:" + distance.ToString());
    	myMoveVelocity.y = myRigidbody2D.velocity.y;//获取刚体y值
        myRigidbody2D.velocity = myMoveVelocity;//重新赋值 
        myMoveVelocity.x = 0f;

        myAttackTimer += Time.deltaTime;
    }

    //巡逻
    void Patrol()
    {
        Filp();
        float t = Mathf.InverseLerp(0, myPatrolDuration, Mathf.PingPong(Time.time, myPatrolDuration));//反向差值得到时间点
        Vector2 positoin = Vector2.Lerp(myPatrolStart, myPatrolEnd, t);//得到时间点对应位置
        myMoveVelocity.x = (positoin - (Vector2)transform.position).normalized.x * speed;
    }
    void Search(){
        /*
        if(distance<=searchRadius){
            Debug.Log("find you!");
            myTarget = playerTransform;
        }
        */
        //搜索圆形半径内玩家
        Collider2D collider = Physics2D.OverlapCircle(transform.position, searchRadius, searchLayer);
        if (collider)//如果检测到
        {
            Debug.Log("find you!");
            myTarget = collider.transform;
        }
    }

    void Track(){
        Filp();
        distance = Vector3.Distance(transform.position, playerTransform.position);
        if(distance<= attackRadius){
            Attack();
        }
        else if(distance < loseRadius){
            myMoveVelocity.x = (myTarget.position - transform.position).normalized.x * trackSpeed;
            myAnimator.SetBool("Attack",false);
        }
        else{
            myTarget=null;
            myAnimator.SetBool("Attack",false);
        }
    }

    void Attack()
    {
        
        if (myAttackTimer >= attackInterval)
        {
            myAttackTimer = 0f;
            Debug.Log("Attack");
            myAnimator.SetBool("Attack",true);
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
    private void TakeDamage()
    {

        if (destroyable)
        {
            HealthPoints -= 1;
        }

        if (HealthPoints <= 0)
        {
            myAnimator.SetTrigger("Die");
            Destroy(gameObject);
            Instantiate(collectObject, transform.position, Quaternion.identity);
        }
    }
    void Filp(){
        bool HasXAxisSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        if(HasXAxisSpeed)
        {
            if(myRigidbody2D.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (myRigidbody2D.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
