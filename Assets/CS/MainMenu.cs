using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button btnStart;  //开始按钮

    void Start()
    {
        //按钮监听，为按钮点击后添加一个链接，按钮点击后触发
        btnStart.onClick.AddListener(GoScene);
    }

    //跳转到Game场景
    void GoScene()
    {
        SceneManager.LoadScene("Game");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //按下按钮，退出游戏
    public void QuitGame()
    {
        Application.Quit();
    }
}
