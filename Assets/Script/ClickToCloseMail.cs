using UnityEngine;

public class ClickToCloseMail : MonoBehaviour
{
    // 需要在Inspector中关联MailManager
    public MailManager mailManager;

    void OnMouseDown()
    {
        Debug.Log("点击了关闭邮件显示区域！");
        if (mailManager != null)
        {
            mailManager.OnCloseMailClick();
        }
    }
}