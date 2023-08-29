using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Transform eye;  //摄像机坐标
    float speedMove = 3f;  //主角移动速度
    float speedHorizontal = 3f;  //主角横移速度
    float speedAngle = 270;   //主角视角旋转速度
    float minAngle = -40;  //最小旋转角度
    float maxAngle = 70;  //最大旋转角度
    float yRote;  //角色上下视角旋转
    const float GRA = -9.8f;  //重力
    CharacterController player;  //主角

    float hp = 100;  //主角血量
    public Slider hpline;  //主角血条
    public Slider powerline;  //主角体力条
    public GameObject canvasDeadUI;  //角色死亡UI
    public GameObject canvas;  //主界面UI
    public Transform handPoint; //定义角色手上道具的位置
    GameObject bombPrefab;  //手雷预制体
    public GameObject powerUI;  //体力耗尽UI

    void Awake()
    {
        eye = transform.Find("Eye");  //获取摄像机
        handPoint = eye.Find("胳膊").Find("HandPoint");  //查找主角手上道具的生成点
        player = GetComponent<CharacterController>();  //获取主角身上的角色控制器
        bombPrefab = Resources.Load<GameObject>("Prefab/Bomb");  //找到手雷预制体
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PowerLineCtrl();
        UserHandTool();
    }

    private void Move()
    {
        //体力<1时不能移动，显示体力耗尽UI
        if (powerline.value < 1)
        {
            powerUI.SetActive(true);
            return;
        }
        else
        {
            powerUI.SetActive(false);
        }
        //虚拟轴
        //控制角色移动
        float y = Input.GetAxis("Vertical");  //代表前后，前为1，后为-1，不动为0
        float x = Input.GetAxis("Horizontal");  //代表左右
        player.Move(transform.forward * Time.deltaTime * y * speedMove);
        player.Move(transform.right * Time.deltaTime * x * speedHorizontal);
        if (Input.GetMouseButton(1))
        {
            //控制角色视角旋转
            float xRote = Input.GetAxis("Mouse X");  //左右旋转
            transform.Rotate(transform.up * speedAngle * xRote * Time.deltaTime);
            //上下视角旋转
            yRote -= Input.GetAxis("Mouse Y");
            yRote = Mathf.Clamp(yRote, minAngle, maxAngle);  //旋转视角角度限制
            eye.localEulerAngles = new Vector3(yRote, 0, 0);  //摄像机的自身坐标旋转
            player.Move(transform.up * GRA * Time.deltaTime);  //受重力下落
        }
    }

    //体力条控制方法
    void PowerLineCtrl()
    {
        if (Input.anyKey)
        {
            SliderNum(powerline, -0.05f);
        }
        else
        {
            SliderNum(powerline, 0.3f);
        }
    }

    //血条和体力条控制方法
    public void SliderNum(Slider slider,float f)
    {
        slider.value += f;
        if (slider.name == "SliderHp" && slider.value == 0)
        {
            canvasDeadUI.SetActive(true);
            canvas.SetActive(false);
            gameObject.tag = "Untagged";
            speedMove = 0;
            speedHorizontal = 0;
            speedAngle = 0;
            yRote = 0;
        }
    }

    //空手使用道具方法
    void UserHandTool()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (Main.toolType)
            {
                //使用手雷
                case ToolType.Bomb:
                    ThrowBomb();
                    break;
                //使用血包，回复血量
                case ToolType.HealthBox:
                    SliderNum(hpline, 50);
                    Main.UseTool("HealthBox", null);
                    break;
            }
        }
    }

    //扔手雷方法
    private void ThrowBomb()
    {
        GameObject go = Main.UseTool("Bomb", bombPrefab);  //生成手雷
        go.transform.position = Main.handPoint.position;  //手雷位置初始化
        go.GetComponent<Rigidbody>().AddForce(eye.transform.forward * 600);  //扔出手雷
    }
}
