using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerController1 : Enemy
{
    public float speed;
	public float waitTime;
	public Transform[] movPos;

	private int i=0;
	private bool movingRight=true;
	private float wait;
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        base.FixedUpdate();
        Patrol();
    }

    public void Patrol(){
    	transform.position = 
            Vector2.MoveTowards(transform.position, movPos[i].position, speed * Time.deltaTime);

    	if (Vector2.Distance(transform.position, movPos[i].position) < 0.1f){
            if (waitTime <= 0){
                if (movingRight == true){
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else{
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }

                if (i == 0){
                    i = 1;
                }
                else{
                    i = 0;
                }

                waitTime = wait;
            }
            else{
                waitTime -= Time.deltaTime;
            }
        }
    }
}
