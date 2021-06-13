using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerAttack : MonoBehaviour
{
  [SerializeField] public float attackDamage = 1.0f;
  public float time;
  public float startTime;
  private Animator anim;
  private PolygonCollider2D col;
  private GameObject player;

  private void Start()
  {
    anim = GameObject.Find("Flyer").GetComponent<Animator>();
    col = GetComponent<PolygonCollider2D>();
  }
  private void FixedUpdate()
  {
  }

  public void Attack()
  {
    anim.SetBool("Attack", true);
    StartCoroutine(disableHitBox());
  }
  IEnumerator startAttack()
  {
    yield return new WaitForSeconds(startTime);
    col.enabled = true;
    StartCoroutine(disableHitBox());
  }
  IEnumerator disableHitBox()
  {
    yield return new WaitForSeconds(time);
    col.enabled = false;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    Debug.Log("213");
    if (other.gameObject.CompareTag("Player"))
    {
      other.GetComponent<HealthBar>().TakeDamage(attackDamage);
    }
  }
}
