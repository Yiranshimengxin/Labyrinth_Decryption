using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bow : MonoBehaviour
{
    public GameObject arrowPrefab;  //弩箭预制体
    Transform point;  //弩箭生成点
    Transform canvas;  //血量画布
    Slider slider;  //血量

    bool isDeath;  //是否存活
    float time;  //临时时间变量
    float shootTime = 1;  //攻击间隔
    float attDis = 12;  //索敌范围
    float hp = 100;  //弩箭血量

    private void Start()
    {
        canvas = transform.Find("Canvas");
        slider=canvas.GetComponentInChildren<Slider>();
        slider.maxValue = hp;
        slider.value = hp;
        point = transform.Find("Point");
    }

    private void Update()
    {
        //判断主角是否死亡
        if (isDeath)
        {
            return;
        }
        //弩箭看向主角
        canvas.LookAt(Main.player);
        slider.value += 0.1f;  //弩箭回血
        TestPlayer();
    }

    //检测玩家方法
    private void TestPlayer()
    {
        if (!Main.TestDistance(transform.position, attDis))
        {
            return;
        }
        Vector3 dir = Main.player.position - transform.position;
        Ray ray = new Ray(transform.position, dir);  //向主角所在的位置发生射线
        RaycastHit hit;
        //射线检测
        if(Physics.Raycast(ray,out hit, attDis))
        {
            //如果是玩家，则发射弩箭
            if (hit.collider.tag == "Player")
            {
                Shoot(dir);
            }
        }
    }

    //发射弩箭方法
    private void Shoot(Vector3 dir)
    {
        //模型纠错，模型的坐标轴是反的
        Quaternion eular=Quaternion.LookRotation(-dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, eular, 0.04f);  //弩箭转向
        //如果转向到一定阶段，发射弩箭
        if (Vector3.Angle(transform.forward, -dir) > 5)
        {
            return;
        }
        //攻击间隔
        if (Time.time - time < shootTime)
        {
            return;
        }
        time = Time.time;
        GameObject arrow = Instantiate(arrowPrefab, point.position, point.rotation);  //生成弩箭
        arrow.GetComponent<Rigidbody>().AddForce(-arrow.transform.forward * 1000f);  //发射弩箭
        Destroy(arrow, 5);  //销毁弩箭
    }

    //弩箭造成伤害方法
    public void BowHurt(float f)
    {
        if (isDeath)
        {
            return;
        }
        //造成伤害
        slider.value -= f;
        if (slider.value == 0)
        {
            isDeath = true;
            Destroy(canvas.gameObject);  //销毁弩箭的血条
        }
    }
}
