using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public float timeCount;//计时器
    public float timeCD;//技能cd
    public Image image;//遮罩图片
    public Text text;//计时文本
    public Button btn;//定义出技能按钮
    public bool isCooling;//是否是冷却状态
                          // Use this for initialization
    void Start()
    {

    }
    public void BtnEvent()
    {
        if (!isCooling)
        {
            image.fillAmount = 1;//按下按钮式遮罩图片的fillAmount=1;
            image.gameObject.SetActive(true);//显示遮罩图片
            text.gameObject.SetActive(true);//显示数字文本框
            text.text = timeCD.ToString("f1");//文本框显示的数字并保留一位小数
            isCooling = true;
            timeCount = timeCD;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (isCooling)
        {
            timeCount -= Time.deltaTime;//计时器赋值
            image.fillAmount = timeCount / timeCD;//遮罩图片的fillAmount赋值
            text.text = timeCount.ToString("f1");//给文本框赋值保留一位小数
            if (timeCount <= 0)
            {
                isCooling = false;
                image.gameObject.SetActive(false);
                text.gameObject.SetActive(false);

            }

        }
    }
}
