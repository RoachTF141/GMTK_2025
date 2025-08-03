using UnityEngine;

public class StartButtonHandler : MonoBehaviour
{
    // 引用GameManager，以便调用其公共方法
    public GameManager gameManager;

    void OnMouseDown()
    {
        Debug.Log("开始按钮被点击了！");
        if (gameManager != null)
        {
            for (int i = 0; i < 4; i++)
            {
                GameManager.Instance?.ReportItemClicked();
            }
        }
    }
}