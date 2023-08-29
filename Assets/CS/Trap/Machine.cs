using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Machine : MonoBehaviour
{
    protected bool isOpen;  //判断机关是否是打开状态
    public ToolType[] states;  //判定道具的名字（数组）
    public GameObject beGetObj;  //被玩家拿的道具

    //鼠标点击方法，鼠标点击到哪个物体，哪个物体就调用这个方法
    private void OnMouseDown()
    {
        //机关离角色太远
        if (!Main.TestDistance(transform.position, 3))
        {
            print("Too far");
            return;
        }
        //机关已经开启
        if (isOpen)
        {
            print("Machine is open!");
        }
        //道具使用错误
        if (!TestToolState())
        {
            print("Tool wrong");
            return;
        }
        OpenMachine();
    }

    //虚函数
    protected virtual void OpenMachine()
    {
        string toolName = Main.toolType.ToString();  //通过枚举获取当前道具的名字
        GameObject toolObj = Main.handPoint.Find(toolName).gameObject;  //查找当前角色手上的道具
        GameObject go = Main.UseTool(toolName, toolObj);
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
        isOpen = true;
        //达成条件后，使物体碰撞体显形
        if (TestFinish())
        {
            beGetObj.GetComponent<Collider>().enabled = true;
        }
    }

    //验证道具是否被消耗掉
    protected bool TestFinish()
    {
        //遍历道具上的所有子物体
        foreach(Transform item in transform.parent)
        {
            //如果道具上所有子物体的 
            if (!item.GetComponent<Machine>().isOpen)
            {
                return false;
            }
        }            
        return true;
    }

    //判断主角手上的道具类型
    protected bool TestToolState()
    {
        //循环检测道具类型
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i] ==Main.toolType)
            {
                return true;
            }
        }
        return false;
    }
}
