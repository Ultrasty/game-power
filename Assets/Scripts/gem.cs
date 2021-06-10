using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gem : MonoBehaviour
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
    private void OnDestroy()
    {
        anim.SetBool("isKey", true);
    }
}
