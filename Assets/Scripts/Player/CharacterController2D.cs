using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    public Image time_mask;
    public Image dup_mask;
    public GameObject bulletPrefab;
    public GameObject dupPrefab;

    public GameObject pool;

    private BoxCollider2D boxCollider;

    private Vector2 velocity;

    private bool grounded;

    private bool faced_dir = true;//true=right,false=left

    public bool bulletTime = false;

    public int bullet_time_length = 1;

    public float bullet_time_remain = 1;

    public int bullet_time_cd = 5;

    public float bullet_time_cd_remain = 0;

    public int duplicate_time_length = 5;
    public float duplicate_time_remain = 5f;

    public int duplicate_time_cd = 3;
    public float duplicate_time_cd_remain = 0;

    public bool have_duplicate = false;
    private Animator anim;
    private FiniteStateMachine fsm;
    private Rigidbody2D rb;
    public Vector2 duplicate_pos;

    private void Awake()
    {      
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        anim= GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fsm = GetComponent<FiniteStateMachine>();
    }

    private void Update()
    {

        if(have_duplicate)
        {
            duplicate_time_remain -= Time.deltaTime;
            dup_mask.fillAmount = (duplicate_time_length-duplicate_time_remain) / duplicate_time_length;
            if(duplicate_time_remain<=0||Input.GetKeyDown(KeyCode.K))
            {
                Destroy(GameObject.FindGameObjectWithTag("Dup"));
                have_duplicate = false;
                transform.position = duplicate_pos;
                duplicate_time_remain = duplicate_time_length;

            }
        }
        else
        {
            if (duplicate_time_cd_remain > 0)
            { 
                duplicate_time_cd_remain -= Time.deltaTime;
                dup_mask.fillAmount = duplicate_time_cd_remain / duplicate_time_cd;
            }
            if(Input.GetKeyDown(KeyCode.K))
            {
                have_duplicate = true;
                GameObject dup = Instantiate(dupPrefab, transform.GetChild(1));
                duplicate_pos = transform.position;
                duplicate_time_cd_remain = duplicate_time_cd;
                transform.GetChild(1).DetachChildren();
            }
        }
        if(bulletTime)
        {
            bullet_time_remain -= Time.deltaTime;
            time_mask.fillAmount = (bullet_time_length-bullet_time_remain) / bullet_time_length;
            if (Input.GetKeyDown(KeyCode.J) || bullet_time_remain <= 0)
            {
                Time.timeScale = 1f;
                
                bullet_time_cd_remain = bullet_time_cd;
                
                bulletTime = false;
            }

        }
        else
        {
            if (bullet_time_remain <= bullet_time_length)
            { 
                bullet_time_remain += 2 * Time.deltaTime;
                time_mask.fillAmount = (bullet_time_length-bullet_time_remain) / bullet_time_length;
            }
            if(bullet_time_cd_remain>=0)
            {
                bullet_time_cd_remain -= Time.deltaTime;
                time_mask.fillAmount = bullet_time_cd_remain / bullet_time_cd;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    Time.timeScale = 0.3f;
                    bulletTime = true;
                }
            }
            
        }

        //change direction 
        float moveInput = Input.GetAxisRaw("Horizontal");
        if(moveInput>0)
        {
            faced_dir = true;
        }
        else if(moveInput<0)
        {
            faced_dir = false;
        }

        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if ((!(info.IsName("Attack1")||info.IsName("Attack2")) ) || info.normalizedTime >= 1.0f)
        //shoot
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                // if the player pressed space (exclude holding key down)
                GameObject[] other_bullets = GameObject.FindGameObjectsWithTag("Bullet");
                GameObject go = Instantiate(bulletPrefab, transform.GetChild(1));
                rb.velocity = new Vector2(0, 0);
                fsm.state = FiniteStateMachine.State.attacking;
                anim.SetBool("Attack1", true);
                foreach (GameObject bu in other_bullets)
                {
                    Physics2D.IgnoreCollision(go.GetComponent<Collider2D>(), bu.GetComponent<Collider2D>());
                }
                Vector3 move_vec = new Vector3();
                if (faced_dir)
                {
                    move_vec.x = 5;
                }
                else
                {
                    move_vec.x = -5;
                }
                go.transform.position += move_vec;
                transform.GetChild(1).DetachChildren();
                Bullet bullet = go.GetComponent<Bullet>();
                int shoot_dir = faced_dir ? 1 : -1;
                bullet.targetVector = new Vector2(shoot_dir, 0);
            }
        }

    }
}
