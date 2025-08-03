using UnityEngine;

public class SendButton : MonoBehaviour
{
    public MailManager mailManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        // 打印出进入触发器的对象的名称和Tag，帮助我们调试
        Debug.Log("OnTriggerEnter2D triggered! Object: " + other.gameObject.name + ", Tag: " + other.gameObject.tag);

        // 检查碰撞体是否是可拖动的邮件用户
        if (other.gameObject.CompareTag("MailUser"))
        {
            Debug.Log("Matched 'MailUser' tag! Hiding object and notifying MailManager.");

            // 隐藏被拖动的邮件用户
            other.gameObject.SetActive(false);

            // 通知MailManager，一个邮件用户被发送了
            if (mailManager != null)
            {
                mailManager.MailSent();
            }
        }
    }
}