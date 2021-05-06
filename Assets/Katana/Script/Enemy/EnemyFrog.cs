using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : Enemy 
{
    public float speed;//移动速度
    public float waitTime;//转身速度
    public Transform[] movePos;//移动范围

    private int i = 0;//定位移动范围数组游标
    private bool movingRight = true;//移动方向
    private float wait;//等待时间
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        wait = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        //向目标点移动
        transform.position = Vector2.MoveTowards(transform.position, movePos[i].position, speed * Time.deltaTime);

        //若即将重合
        if(Vector2.Distance(transform.position, movePos[i].position) < 0.1f)
        {
            if (waitTime >= 0)
            {
                waitTime -= Time.deltaTime;//重置移动cd(实现停顿)
            }
            else
            {
                if (movingRight)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);//转向
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);//转向
                    movingRight = true;
                }

                //切换移动目标点
                if (i == 0)
                    i = 1;
                else
                    i = 0;

                waitTime = wait;
            }
        }
    }
}
