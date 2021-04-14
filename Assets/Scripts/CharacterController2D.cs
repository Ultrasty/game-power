using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 9;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 30;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 70;

    [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    float jumpHeight = 4;

    public GameObject bulletPrefab;

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

    public Vector2 duplicate_pos;

    private void Awake()
    {      
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(have_duplicate)
        {
            duplicate_time_remain -= Time.deltaTime;
            if(duplicate_time_remain<=0||Input.GetKeyDown(KeyCode.C))
            {
                have_duplicate = false;
                transform.position = duplicate_pos;
                duplicate_time_remain = duplicate_time_length;
            }
        }
        else
        {
            if (duplicate_time_cd_remain >= 0)
                duplicate_time_cd_remain -= Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.C))
            {
                have_duplicate = true;
                duplicate_pos = transform.position;
                duplicate_time_cd_remain = duplicate_time_cd;
            }
        }
        if(bulletTime)
        {
            bullet_time_remain -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Z) || bullet_time_remain <= 0)
            {
                Time.timeScale = 1f;
                bullet_time_cd_remain = bullet_time_cd;
                
                bulletTime = false;
            }

        }
        else
        {
            if (bullet_time_remain <= bullet_time_length)
                bullet_time_remain += 2 * Time.deltaTime;
            if(bullet_time_cd_remain>=0)
            {
                bullet_time_cd_remain -= Time.deltaTime;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Time.timeScale = 0.1f;
                    bulletTime = true;
                }
            }
            
        }
        //jump operations
        if (grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            }
        }
        else
        {
            velocity.y += Physics2D.gravity.y * Time.deltaTime;
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

        //shoot
        if (Input.GetKeyDown(KeyCode.X))
        {
            // if the player pressed space (exclude holding key down)
            GameObject go = Instantiate(bulletPrefab,transform.GetChild(1));

            transform.GetChild(1).DetachChildren();
            Bullet bullet = go.GetComponent<Bullet>();
            int shoot_dir = faced_dir ? 1 : -1;
            bullet.targetVector = new Vector2(shoot_dir, 0);
        }


        //move
        float acceleration = grounded ? walkAcceleration : airAcceleration;
        float deceleration = grounded ? groundDeceleration : 0;

        if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, acceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
        }

        transform.Translate(velocity * Time.deltaTime);


        //collidation detect
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        grounded = false;
        foreach (Collider2D hit in hits)
        {
            if (hit == boxCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
            }

            if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
            {
                grounded = true;
            }
        }

    }
}
