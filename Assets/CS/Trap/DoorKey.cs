using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : Door
{
    public bool isOpen;  //判断机关是否是打开状态

    void Update()
    {
        if (!isOpen)
        {
            return;
        }
        OpenDoor();
    }
}
