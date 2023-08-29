using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BtnTools : MonoBehaviour
{
    public int toolGetNum;  //道具点一次会拿到几个
    Text text;  //数量文本
    int toolNum;  //道具数量上限

    public void Init()
    {
        //更新道具数量文本
        text=GetComponentInChildren<Text>();  //获取自身及子物体身上的文字
        toolNum = toolGetNum;
        text.text=toolNum.ToString();        
        //道具按钮监听事件，按下按钮触发后续方法
        GetComponent<Button>().onClick.AddListener(BtnClick);
    }

    //按下背包里道具按钮方法
    void BtnClick()
    {
        string toolName = name.Remove(0, 3);  //删除道具名字的前部分
        ShowTool(toolName,true);  //显示道具
        //将字符串转换为枚举类型
        Main.toolType = (ToolType)Enum.Parse(typeof(ToolType), toolName);
    }

    //显示道具
    void ShowTool(string n,bool b)
    {
        Main.AllToolVisible(false);  //使手上的道具全部隐藏
        GameObject tool = Main.handPoint.Find(n).gameObject;  //定义当前手上的道具
        tool.SetActive(b);  //使其隐藏或显示
    }

    //获取道具的数量
    public int GetToolNum()
    {
        return toolNum;
    }

    //添加道具的数量
    public void AddToolNum(int num)
    {
        toolNum += num;
        text.text = toolNum.ToString();
    }
}
