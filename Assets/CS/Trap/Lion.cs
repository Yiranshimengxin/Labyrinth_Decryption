using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : Machine
{
    public Transform otherLion;

    protected override void OpenMachine()
    {
        //石狮子旋转
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 45, 0);
        Test();
    }

    //判断两个石狮子之间的位置是否是正确的位置
    void Test()
    {
        //两个石狮子位置正确则开门，同时消除石盒子的盒盖
        if (SetDirection() && otherLion.GetComponent<Lion>().SetDirection())
        {
            isOpen = true;
            otherLion.GetComponent<Lion>().isOpen = true;
            beGetObj.SetActive(false);
        }
    }

    //判断石狮子是否看向正确的方向
    public bool SetDirection()
    {
        //计算石狮子移动的位置
        Vector3 dir = otherLion.position - transform.position;
        //判断两个石狮子的位置是否是面对面
        if (Vector3.Angle(dir, transform.right) < 5)
        {
            return true;
        }
        return false;
    }
}
