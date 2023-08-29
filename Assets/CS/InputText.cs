using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InputText : MonoBehaviour
{
    public GameObject passwordRightUIPrefab;  //定义登录成功UI预制体
    public GameObject passwordErrorUIPrefab;   //定义登录失败UI预制体

    public Button btnStart;  //开始按钮
    public Button btnReadme;  //说明按钮

    string inputUserName;  //定义输入框输入内容的对象
    string inputPassword;  //定义输入框输入内容的对象
    static string userName = "依然是萌新";  //正确的用户名
    static string password = "123456";  //正确的密码

    void Start()
    {
        StartCoroutine(CheckText());
    }

    //读取玩家输入的用户名和密码
    IEnumerator CheckText()
    {
        while (true)
        {
            inputUserName = GameObject.Find("UserInput").GetComponent<InputField>().text;  //获取玩家输入的用户名
            inputPassword = GameObject.Find("PasswordInput").GetComponent<InputField>().text;  //获取玩家输入的密码
            yield return new WaitForSeconds(0.01f);
        }
    }

    void JudgeCorrect()
    {
        //判断用户名和密码是否正确
        if (inputUserName == userName && inputPassword == password)
        {
            //成功登录
            passwordErrorUIPrefab.SetActive(false);
            passwordRightUIPrefab.SetActive(true);
            btnStart.enabled = true;
            btnReadme.enabled = true;
            Invoke("CloseInterface", 2f);
        }
        else if (inputUserName != userName || inputPassword != password)
        {
            //登录失败
            passwordErrorUIPrefab.SetActive(true);
            Invoke("CloseErrorUI", 2f);
            Cancel();
        }
    }

    //关闭登录界面，进入游戏主界面
    void CloseInterface()
    {
        gameObject.SetActive(false);
    }

    //延时关闭密码错误UI
    void CloseErrorUI()
    {
        passwordErrorUIPrefab.SetActive(false);
    }

    //登录按钮方法
    public void Log()
    {
        JudgeCorrect();
    }

    //取消按钮方法，清除已输入的文本
    public void Cancel()
    {
        GameObject.Find("UserInput").GetComponent<InputField>().text = null;
        GameObject.Find("PasswordInput").GetComponent<InputField>().text = null;
    }
}
