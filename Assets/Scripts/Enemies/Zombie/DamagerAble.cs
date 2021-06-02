using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagerable : MonoBehaviour
{
    [System.Serializable]
    public class DamageableEvent : UnityEvent<Damager, Damagerable>{}
    public DamageableEvent onTakeDamgae;
    public DamageableEvent onDie;

    public float maxHealth;
    public float cuurentHealth { get; protected set; }
    // Start is called before the first frame update
    public void TakeDamage(Damager damager)
    {
        cuurentHealth -= damager.damage;
        onTakeDamgae.Invoke(damager, this);
        if(cuurentHealth <= 0)
        {
            onDie.Invoke(damager, this);
        }
    }
}
