using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    protected Vector3 targetPos;  //门的起始坐标
    protected Vector3 startPos;  //门要移动到的坐标

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + transform.forward * 2.5f;
    }

    void Update()
    {
        OpenDoor();
    }

    //石墙打开
    protected void OpenDoor()
    {
        //如果墙壁和人物的距离小于3m
        if (Main.TestDistance(startPos, 3))
        {
            //石墙移动
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.08f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPos, 0.08f);
        }
    }
}
