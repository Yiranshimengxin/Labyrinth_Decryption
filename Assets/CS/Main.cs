using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

//定义全部道具名称（枚举）
public enum ToolType
{
    BallRed,BallGreen,BallYellow,BallWhite,BallBlack,
    BallStone,PicYZ,PicYX,PicYC,PicZF,
    Brush,Bomb,Torch,HealthBox,Hand
}

public class Main : MonoBehaviour
{
    public static Transform player;  //定义静态角色坐标
    public static ToolType toolType { set; get; }  //定义静态类型道具变量（属性）
    public static Transform bag;  //定义背包
    public static Transform handPoint;  //定义角色手上道具的位置
    public Transform canvas;  //定义背包的面板


    // Start is called before the first frame update
    void Start()
    {
        toolType = ToolType.Hand;  //初始为空手
        bag = canvas.Find("Panel").Find("Bag");  //查找背包父物体
        player = GameObject.Find("Player").transform;  //获取角色坐标
        handPoint = player.GetComponent<Player>().handPoint;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //判断角色离墙的距离
    public static bool TestDistance(Vector3 pos,float distance)
    {
        return Vector3.Distance(player.position, pos) < distance;
    }

    //判断当前的道具状态是否是Hand（空手）
    public static bool IsHand()
    {
        return toolType == ToolType.Hand;
    }

    //遍历手上的所有道具
    public static void AllToolVisible(bool b)
    {
        foreach(Transform item in handPoint)
        {
            item.gameObject.SetActive(b);
        }
    }

    //道具为手状态时
    public void Hand()
    {
        toolType=ToolType.Hand;
        AllToolVisible(false);
    }

    //使用道具方法
    public static GameObject UseTool(string toolName,GameObject obj)
    {
        //根据名字查找道具
        BtnTools btnTools = bag.Find("Btn" + toolName).GetComponent<BtnTools>();
        //如果道具的数量大于0
        if (btnTools.GetToolNum() > 0)
        {
            GameObject go = null;
            if (obj)
            {
                //生成道具
                go=Instantiate(obj);
            }
            //道具数量减1
            btnTools.AddToolNum(-1);
            if(btnTools.GetToolNum() == 0)
            {
                //销毁道具和背包里的道具
                Destroy(handPoint.Find(toolName).gameObject);
                Destroy(btnTools.gameObject, 0.1f);
                toolType=ToolType.Hand;  //转换为空手
            }
            return go;
        }
        else
        {
            return null;
        }
    }

    public void AgainGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
