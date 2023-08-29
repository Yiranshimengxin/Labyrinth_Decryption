using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject bombEffect;  //爆炸效果预制体
    float attDis = 10;  //爆炸范围
    float attMax = 100;  //爆炸伤害

    private void Start()
    {
        Invoke("BombEffect",3);
        Destroy(gameObject, 3);  //扔出手雷后延迟爆炸
    }

    //手雷爆炸方法
    void BombEffect()
    {
        GameObject effect=Instantiate(bombEffect,transform.position,transform.rotation);  //生成爆炸粒子
        //取得爆炸范围内所有物体的碰撞体，再通过Layer图层识别出弩盘，取到弩盘的碰撞体
        Collider[] bowColl = Physics.OverlapSphere(transform.position, attDis, LayerMask.GetMask("Bow"));
        foreach (Collider c in bowColl)
        {
            c.GetComponent<Bow>().BowHurt(Hurt(c.gameObject));
        }
        Destroy(effect, 3);
    }

    //手雷爆炸造成伤害方法
    float Hurt(GameObject go)
    {
        float distance=Vector3.Distance(transform.position,go.transform.position);
        if (distance >= attDis)
        {
            return 0;
        }
        else
        {
            //伤害随着爆炸中心距离衰减
            return attMax - distance * attMax / attDis;
        }
    }
}
