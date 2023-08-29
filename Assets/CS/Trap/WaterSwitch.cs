using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterSwitch : Machine
{
    public Transform waterPlane;  //水面
    Vector3 targetPos;
    Vector3 waterPos;

    private void Start()
    {
        targetPos = transform.position - Vector3.up * 0.05f;
        waterPos = waterPlane.position - Vector3.up * 0.4f;
    }

    protected override void OpenMachine()
    {
        isOpen=true;
    }

    private void Update()
    {
        if (isOpen)
        {
            waterPlane.position = Vector3.Lerp(waterPlane.position, waterPos, 0.005f);
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.005f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 200, 0), 0.005f);
        }
    }
}
