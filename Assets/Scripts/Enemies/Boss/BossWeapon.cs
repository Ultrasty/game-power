using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
  public int attackDamage = 20;
  public int enragedAttackDamage = 30;

  public Vector3 attackOffset;
  public float attackRange = 1f;
  public LayerMask attackMask;
  public AudioSource attackSource;
  public AudioSource enragedAttackSource;
  public Transform enemyFirePoint;

  public void Attack()
  {
    Vector3 pos = transform.position;
    pos += transform.right * attackOffset.x;
    pos += transform.up * attackOffset.y;
    attackSource.Play();
    Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
    if (colInfo != null)
    {

      colInfo.GetComponent<HealthBar>().TakeDamage(attackDamage);
    }

  }

  public void EnragedAttack()
  {
    Vector3 pos = transform.position;
    pos += transform.right * attackOffset.x;
    pos += transform.up * attackOffset.y;
    enragedAttackSource.Play();
    Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
    if (colInfo != null)
    {
      colInfo.GetComponent<HealthBar>().TakeDamage(enragedAttackDamage);
    }
  }

  void OnDrawGizmosSelected()
  {
    Vector3 pos = transform.position;
    pos += transform.right * attackOffset.x;
    pos += transform.up * attackOffset.y;

    Gizmos.DrawWireSphere(pos, attackRange);
  }
}
