using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    GameObject btnTool;  //定义工具类型按钮

    void Start()
    {
        //到Resources/Prefab/Btn文件夹查找物体
        btnTool = Resources.Load<GameObject>("Prefab/Btn" + name);
    }

    //鼠标点击事件
    private void OnMouseDown()
    {
        //道具离角色过于远
        if (!Main.TestDistance(transform.position, 3))
        {
            print("Too far");
            return;
        }
        //判断角色是否是空手，非空手不能拾取道具
        if (!Main.IsHand())
        {
            print("hand not empty");
            return;
        }
        if (Main.handPoint.Find(name))
        {
            BtnTools btn = Main.bag.Find(btnTool.name).GetComponent<BtnTools>();
            btn.AddToolNum(btn.toolGetNum);
            Destroy(gameObject);
        }
        else
        {
            PickUp();
        }
    }

    //拾取道具，将道具放入Bag的子物体
    void PickUp()
    {
        //场景里的道具消失，然后成为主角Hand的子物体，同时消除其存在
        transform.position = Main.handPoint.position;
        transform.SetParent(Main.handPoint, true);
        transform.localEulerAngles = Vector3.zero;
        gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
        enabled=false;
        //在背包面板里生成道具按钮，按钮为背包的子物体
        GameObject btn = Instantiate(btnTool, Main.bag);
        btn.name=btnTool.name;
        btn.GetComponent<BtnTools>().Init();
    }

    public void PickPic()
    {
        PickUp();
    }
}
