using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : Machine
{
    protected Vector3 doorStartPos;
    Vector3 startPos = new Vector3(0, -0.055f, -0.03f);

    private void Start()
    {
        
    }

    protected override void OpenMachine()
    {
        //判断画框的子物体是否为0，为0则是空画框，可以放画
        if(transform.childCount == 0)
        {
            if (Main.toolType != ToolType.Hand)
            {
                PickPic();
            }
        }
        else
        {
            if (Main.toolType == ToolType.Hand)
            {
                GetComponentInChildren<Tools>().PickPic();
            }
        }
        if (TestAllPic())
        {
            SetAllPic();
        }
    }

    //取画放画方法
    private void PickPic()
    {
        string str = Main.toolType.ToString();
        Transform t = Main.handPoint.Find(str);
        t.parent = transform;
        t.localEulerAngles = Vector3.zero;
        t.localPosition = startPos;
        Transform btnT = Main.bag.Find("Btn" + str);
        Main.toolType = ToolType.Hand;
        Destroy(btnT.gameObject);
    }

    //检测画放得是否正确方法
    protected bool TestAllPic()
    {
        foreach(Transform t in transform.parent)
        {
            if (t.childCount == 0)
            {
                return false;
            }
            if (t.name != "K" + t.GetChild(0).name)
            {
                return false;
            }
        }
        return true;
    }

    //所有画放置正确，开启暗门
    protected void SetAllPic()
    {
        foreach(Transform t in transform.parent)
        {
            t.GetComponent<Frame>().isOpen = true;
        }
        beGetObj.GetComponent<DoorKey>().isOpen = true;
    }
}
