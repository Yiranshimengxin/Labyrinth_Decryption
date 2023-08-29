using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Machine
{
    //重写机关打开函数
    protected override void OpenMachine()
    {
        GetComponentInParent<DoorKey>().isOpen = true;
        GetComponent<Renderer>().enabled = true; 
    }
}
