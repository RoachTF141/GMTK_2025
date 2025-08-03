using UnityEngine;
using System.Collections;

public class MailManager : MonoBehaviour
{
    // 引用所有的邮件用户对象，方便管理
    public GameObject[] mailUsers;
    // 引用邮件显示界面
    public GameObject mailDisplayPanel;

    private int unsentMailCount;

    public GameObject phonePanel4;
    public GameObject bossObject;
    public GameObject answerPhoneButton; // 接老板电话按钮

    private OfficeManager officeManager;



    void Start()
    {

        // 查找场景中的 OfficeManager 实例
        officeManager = FindObjectOfType<OfficeManager>();

        // 初始化未发送邮件的数量
        unsentMailCount = mailUsers.Length;

        // 设置初始UI状态
        // 邮件显示和电话面板4隐藏
        mailDisplayPanel.SetActive(false);
        phonePanel4.SetActive(false);


        // 老板和接电话按钮显示
        bossObject.SetActive(true);
        answerPhoneButton.SetActive(true);
    }

    // 当一个邮件用户被成功发送时调用
    public void MailSent()
    {
        unsentMailCount--;

        // 检查是否所有邮件都已发送
        if (unsentMailCount <= 0)
        {
            if(officeManager != null)
            {
                officeManager.StartMailSequence(); // 调用 OfficeManager 的新方法
            }

            Debug.Log("所有邮件已发送完毕，关闭邮件显示界面。");
            // 关闭邮件显示界面
            mailDisplayPanel.SetActive(false);




        }
    }


    public void OnAnswerPhoneClick()
    {
        // 隐藏老板和接电话按钮
        bossObject.SetActive(false);
        answerPhoneButton.SetActive(false);
        
        // 显示电话面板4和邮件显示
        phonePanel4.SetActive(true);
        mailDisplayPanel.SetActive(true);
    }

    // 邮件icon的点击事件处理函数
    public void OnMailIconClick()
    {
        Debug.Log("OnMailIconClick called.");
        // 检查未发送邮件的数量
        if (unsentMailCount > 0)
        {
            mailDisplayPanel.SetActive(true);
        }
        else
        {
            mailDisplayPanel.SetActive(true);
            // 确保所有邮件用户对象被隐藏
            foreach(GameObject user in mailUsers)
            {
                user.SetActive(false);
            }
        }
    }

    // 新增：关闭邮件显示的点击事件处理函数
    public void OnCloseMailClick()
    {
        // 隐藏邮件显示界面
        mailDisplayPanel.SetActive(false);
    }

}