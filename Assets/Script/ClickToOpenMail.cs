using UnityEngine;

public class ClickToOpenMail : MonoBehaviour
{
    // 需要在Inspector中关联MailManager
    public MailManager mailManager;

    void OnMouseDown()
    {
        Debug.Log("邮件icon被点击了！");
        if (mailManager != null)
        {
            mailManager.OnMailIconClick();
        }
    }
}