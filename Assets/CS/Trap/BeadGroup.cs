using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadGroup : MonoBehaviour
{
    public static GameObject[,] beadAllDown = new GameObject[4, 4];
    public static GameObject[] beadTop = new GameObject[4];
    public static int answer;  //开门的正确答案
    static int[] answers = new int[4] { 4, 0, 2, 2 };  //下方珠子正确答案
    static Color[] answersColor = new Color[4] { Color.red, Color.green, new Color(1, 1, 0), Color.black };   //下方珠子的正确颜色
    static bool[] answersTop = new bool[4] { true, true, false, true };  //上方珠子的正确答案
    static Color[] answersTopColor=new Color[4] {Color.red,Color.green, Color.white, Color.black};  //上方珠子的正确颜色
    public GameObject door;  //定义暗门物体
    static GameObject _door;

    private void Start()
    {
        GetBead();
        _door = door;  //转换暗门物体为静态
    }

    //将算盘的每个珠子放到一个二维数组里
    void GetBead()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                beadAllDown[i, j] = transform.GetChild(i * 4 + j).gameObject;
            }
        }
        for (int i = 0; i < beadTop.Length; i++)
        {
            beadTop[i] = transform.GetChild(16 + i).gameObject;
        }
    }


    ///////////////////////////////////第一种方法，判断算盘上的珠子是否拨动到正确的位置/////////////////////////////////////////////////////////
    //使算盘上是珠子能进行运算，计算公式为a*10^3+b*10^2+c*10^1+d*10^0
    public static void AddBead(int line, int num)
    {
        //计算答案
        answer += (int)Mathf.Pow(10, (3 - line)) * num;
        print(answer);
        if (answer == 9527)
        {
            print("right");
        }
    }

    /////////////////////////////////////////////////////////第二种方法/////////////////////////////////////////////////////////
    //判断下方珠子是否到正确的地方
    static bool TestBeadOk()
    {
        for (int i = 0; i < answers.Length; i++)
        {
            if (beadAllDown[i, answers[i]].GetComponent<Bead>().isClicked)
            {
                return false;
            }
            if (!beadAllDown[i, answers[i] - 1].GetComponent<Bead>().isClicked)
            {
                return false;
            }
        }
        return true;
    }

    /////////////////////////////////////////////////////////第三种方法/////////////////////////////////////////////////////////
    //在第二种方法的基础上改进，通过标准答案将算盘分成两半，分开判定
    public static bool TestBeadOk2()
    {
        for (int k = 0; k < answers.Length; k++)
        {
            for (int i = 0; i < answers[k]; i++)
            {
                if (!beadAllDown[k, i].GetComponent<Bead>().isClicked)
                {
                    //print(k + "    " + i);
                    return false;
                }
                if (!TestBeadColor(beadAllDown[k, i], answersColor[k]))
                {
                    //print(k + "  c  " + i);
                    return false;
                }
            }
            for (int i = answers[k]; i < 4; i++)
            {
                if (beadAllDown[k, i].GetComponent<Bead>().isClicked)
                {
                    //print(k + "    " + i);
                    return false;
                }
                if (!TestBeadColor(beadAllDown[k, i], Color.white))
                {
                    //print(k + "  c  " + i);
                    return false;
                }
            }
        }
        return true;
    }

    //判断顶部珠子是否到正确的位置
    static bool TestBeadTopOk()
    {
        for (int i = 0; i < answersTop.Length; i++)
        {
            if (beadTop[i].GetComponent<Bead>().isClicked != answersTop[i])
            {
                //print(i);
                return false;
            }
            if (!TestBeadColor(beadTop[i], answersTopColor[i]))
            {
                //print("c"+i);
                return false;
            }
        }
        return true;
    }

    //开启暗门方法
    public static void TestOpenDoor()
    {
        if (!TestBeadOk2())
        {
            return;
        }
        if (!TestBeadTopOk())
        {
            return;
        }
        print("Open!");
        _door.GetComponent<DoorKey>().isOpen = true;
    }

    //判断珠子的颜色是否是正确的颜色
    static bool TestBeadColor(GameObject bead,Color color)
    {
        if (bead.GetComponent<Renderer>().material.color == color)
        {
            return true;
        }
        return false;
    }
}

