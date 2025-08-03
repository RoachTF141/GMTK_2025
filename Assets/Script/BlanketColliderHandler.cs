using UnityEngine;

public class BlanketColliderHandler : MonoBehaviour
{
    // 在Inspector中关联目标区域
    public GameObject dropTarget;

    // 在Inspector中关联要隐藏和显示的对象
    public GameObject awakeImage; // IMG_0932_Awake
    public GameObject sleepImage; // IMG_0931_Sleep

    void OnTriggerEnter2D(Collider2D other)
    {
        // 检查碰撞体是否是指定的拖放目标
        if (other.gameObject == dropTarget)
        {
            Debug.Log("IMG_0930_Blanket collided with target! Hiding Awake and showing Sleep.");

            // 隐藏IMG_0932_Awake
            if (awakeImage != null)
            {
                awakeImage.SetActive(false);
            }

            // 显示IMG_0931_Sleep
            if (sleepImage != null)
            {
                sleepImage.SetActive(true);
            }

            // 成功后，将可拖动对象本身也隐藏
            gameObject.SetActive(false);
        }
        GameManager.Instance?.ReportItemClicked();
        GameManager.Instance?.ReportItemClicked();
        GameManager.Instance?.ReportItemClicked();
        GameManager.Instance?.ReportItemClicked();
    }
}