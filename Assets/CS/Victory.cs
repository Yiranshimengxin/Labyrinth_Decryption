using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Victory : MonoBehaviour
{
    public GameObject canvasVictory;
    public Text textVictory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canvasVictory.SetActive(true);            
            Color color = new Color(Random.Range(0,255), Random.Range(0, 255), Random.Range(0, 255), 255);
            textVictory.color = color;
            Invoke("OffCanvas", 10);
        }
    }

    void OffCanvas()
    {
        canvasVictory.SetActive(false);
    }


}
