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
    anim.SetBool("isTruck", true);
  }

  // Update is called once per frame
  void Update()
  {

  }
  public void Interact()
  {

  }

}
