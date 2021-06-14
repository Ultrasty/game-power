using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    public GameObject beam;
    bool isBeam;
    public int cost;
    // Start is called before the first frame update
    void Start()
    {
        isBeam = true;
        InvokeRepeating("damage", 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void damage()
    {
        if(isBeam == true)
        {
            beam.SetActive(false);
            isBeam = false;
        }
        else
        {
            beam.SetActive(true);
            isBeam = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            HealthBar player = col.gameObject.GetComponent<HealthBar>();
            if (player != null)
            {
                player.TakeDamage(cost);
            }
        }
    }
}
