using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : Enemy
{
    public float speed;
    public float startWaitTime;

    private float waitTime;

    public Transform eagleMovePos;//下次移动位置
    public Transform eagleLeftDownPos;//移动范围
    public Transform eagleRightUpPos;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        waitTime = startWaitTime;
        eagleMovePos.position = GetRandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        //从transform.position移动到movePos.position，速度为speed * Time.deltaTime
        transform.position = Vector2.MoveTowards(transform.position, eagleMovePos.position, speed * Time.deltaTime);
        //判断是否到达下一位置
        if (Vector2.Distance(transform.position, eagleMovePos.position) < 0.1f)
        {
            //到达预定位置后停留多久
            if (waitTime <= 0)
            {
                eagleMovePos.position = GetRandomPos();//移动到下一随机位置
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(eagleLeftDownPos.position.x, eagleRightUpPos.position.x),
            Random.Range(eagleLeftDownPos.position.y, eagleRightUpPos.position.y));
        return rndPos;
    }
}
