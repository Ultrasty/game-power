using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerController : Enemy
{
    [SerializeField] public float speed;
    [SerializeField] public float searchRadius;
    [SerializeField] public float attackRadius;//攻击距离
    [SerializeField] public float attackInterval;//攻击间隔
    [SerializeField] public bool destroyable = true;
    [SerializeField] public float attackDamage = 1.0f;
    public GameObject collectObject;

    private Transform playerTransform;
    Animator myAnimator;
    private bool finding;//是否找到玩家
    private float myAttackTimer;
    private FlyerAttack myAttack;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        myAnimator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        base.FixedUpdate();
        SearchPlayer();
        
        myAttackTimer += Time.deltaTime;


    }

    void SearchPlayer()
    {
        if (playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            //Debug.Log("Dist Vector:" + (transform.position - playerTransform.position).ToString());
            //Debug.Log("distance:" + distance.ToString());
            
            if (distance <= attackRadius)
            {
                //myAttack.Attack();
                Attack();
            }
            else if(distance <= searchRadius)
            {
                //Debug.Log("find you");
                myAnimator.SetBool("Tracking",true);
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
                finding = true;
            }
        }
    }
    
    void Attack()
    {
        
        if (myAttackTimer >= attackInterval)
        {
            myAttackTimer = 0f;
            //Debug.Log("Attack");
            myAnimator.SetBool("Attack",true);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
    private void TakeDamage()
    {

        if (destroyable)
        {
            HealthPoints -= 1;
        }

        if (HealthPoints <= 0)
        {
            myAnimator.SetBool("Die", true);
            Destroy(gameObject);
            Instantiate(collectObject, transform.position, Quaternion.identity);
        }
    }
}
