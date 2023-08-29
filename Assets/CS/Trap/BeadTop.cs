using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadTop : Bead
{
    // Start is called before the first frame update
    void Start()
    {
        //珠子的移动距离
        SetStartValue(transform.forward * 0.2f);
    }

    //重写点击珠子方法，每个珠子代表5
    protected override void BeadClick()
    {
        if (!isClicked)
        {
            transform.position = targetPos;
            //BeadGroup.AddBead(line, 5);
        }
        else
        {
            transform.position = startPos;
            //BeadGroup.AddBead(line, -5);
        }
        isClicked = !isClicked;
        BeadGroup.TestOpenDoor();
    }
}
