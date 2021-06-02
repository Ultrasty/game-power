using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float damage;
    public Vector2 offset;
    public Vector2 size;
    public LayerMask damageableLayer;

    ContactFilter2D myDamageableFilter;
    Collider2D[] myDamageableResults = new Collider2D[8];

    SpriteRenderer mySpriteRenderer;

    bool myEnableDamage;
    bool myTriggerDamageOnce;
    // Start is called before the first frame update
    void Start()
    {
        myDamageableFilter.layerMask = damageableLayer;
        myDamageableFilter.useLayerMask = true;
        myDamageableFilter.useTriggers = false;

        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myEnableDamage) return;
        //只触发一次
        if (myTriggerDamageOnce)
        {
            myEnableDamage = false;
            myTriggerDamageOnce = false;
        }
        Vector2 tmpoffset = offset;
        if (mySpriteRenderer.flipX) tmpoffset.x *= -1;
        Vector2 center = (Vector2)transform.position + tmpoffset;
        Vector2 halfSize = size * 0.5f;
        Vector2 positionA = center - halfSize;
        Vector2 positionB = center + halfSize;

        int count = Physics2D.OverlapArea(positionA, positionB, myDamageableFilter, myDamageableResults);
        if(count > 0)
        {
            for (int i = 0; i < count;i++)
            {
                Collider2D collider = myDamageableResults[i];
                Damagerable damagerable = collider.gameObject.GetComponent<Damagerable>();
                if(damagerable != null)
                {
                    damagerable.TakeDamage(this);
                }
            }
        }
    }
    //攻击范围
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + offset, size);
    }
    public void EnableDamage()
    {
        myEnableDamage = true;
    }
    public void DisableDamage()
    {
        myEnableDamage = false;
    }
    public void TriggerDamageOnce()
    {
        myEnableDamage = true;
        myTriggerDamageOnce = true;
    }
}
