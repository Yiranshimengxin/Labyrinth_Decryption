using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //角色扣血
            Player player = collision.gameObject.GetComponent<Player>();
            player.SliderNum(player.hpline, -10);
        }
        //删除射出的弩箭的刚体和碰撞体
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<Collider>());
        //将弩箭变成主角的子物体
        transform.parent=collision.transform;
    }
}
