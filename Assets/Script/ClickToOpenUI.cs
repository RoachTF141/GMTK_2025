using UnityEngine;

public class ClickToOpenUI : MonoBehaviour
{
    // 需要在Inspector中关联MailManager
    public MailManager mailManager;

    void OnMouseDown()
    {
        Debug.Log("接老板电话被点击了！");
        if (mailManager != null)
        {
            mailManager.OnAnswerPhoneClick();
        }
    }
}