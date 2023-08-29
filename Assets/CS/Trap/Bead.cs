using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bead : Machine
{
    protected int line;  //珠子位于几行
    protected int num;  //珠子位于第几个
    protected Vector3 startPos;  //珠子的初始坐标
    protected Vector3 targetPos;  //珠子的移动坐标

    public bool isClicked;  //珠子是否被点击

    private void Start()
    {
        //每个珠子的位移值
        SetStartValue(-transform.forward * 0.3f);
    }

    protected void SetStartValue(Vector3 v3)
    {
        //求出每个珠子的行和列
        line = int.Parse(name[4].ToString());
        num = int.Parse(name[5].ToString());
        //移动珠子
        startPos = transform.position;
        targetPos = startPos + v3;
    }

    //点击珠子移动，每个珠子代表1
    protected virtual void BeadClick()
    {
        //珠子向上移动
        if (!isClicked)
        {
            if (num == 0)
            {
                //当前目标等于目标坐标
                transform.position = targetPos; 
                //BeadGroup.AddBead(line, 1);
                isClicked = !isClicked;
            }
            else
            {
                if (BeadGroup.beadAllDown[line, num - 1].GetComponent<Bead>().isClicked)
                {
                    transform.position = targetPos;
                    //BeadGroup.AddBead(line, 1);
                    isClicked = !isClicked;
                }
            }
        }
        //珠子向下移动
        else
        {
            if (num == 3)
            {
                //当前目标等于起始坐标
                transform.position = startPos;
                //BeadGroup.AddBead(line, -1);
                isClicked = !isClicked;
            }
            else
            {
                if (!BeadGroup.beadAllDown[line, num + 1].GetComponent<Bead>().isClicked)
                {
                    transform.position = startPos;
                    //BeadGroup.AddBead(line, -1);
                    isClicked = !isClicked;
                }
            }
        }
        BeadGroup.TestOpenDoor();
    }

    //特定道具才能点击珠子
    protected override void OpenMachine()
    {
        if (Main.toolType == ToolType.Hand)
        {
            BeadClick();
        }
        if(Main.toolType == ToolType.Brush)
        {
            PaintColor();
        }
    }

    //给珠子涂色方法
    protected void PaintColor()
    {
        Material mat = GetComponent<Renderer>().material;
        mat.color = Main.handPoint.Find("Brush").GetComponentInChildren<Renderer>().materials[2].color;
        BeadGroup.TestOpenDoor();
    }
}
