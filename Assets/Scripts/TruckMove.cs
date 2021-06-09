using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMove : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Animator anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void activateTruck()
    {
        anim.SetBool("isTruck", true);
    }
}
