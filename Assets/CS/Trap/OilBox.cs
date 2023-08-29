using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBox : Machine
{
    protected override void OpenMachine()
    {
        //获取刷子上刷毛的材质
        Material mat = Main.handPoint.Find("Brush").GetComponentInChildren<Renderer>().materials[2];
        //将刷毛的颜色变为油漆桶里对应的颜色
        mat.color=GetComponent<Renderer>().material.color;
    }
}
