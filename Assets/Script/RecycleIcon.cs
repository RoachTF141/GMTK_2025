using UnityEngine;

public class RecycleIcon : MonoBehaviour
{

    public OfficeManager officeManager;

    // 当有其他2D碰撞体进入时触发
    void OnTriggerEnter2D(Collider2D other)
    {
        // 检查碰撞体是否是坏猫

        if (other.gameObject.CompareTag("BadCat"))
        {
            Debug.Log("一个坏猫被拖到回收站了！");
            
            // 隐藏被拖动的坏猫
            other.gameObject.SetActive(false);

            // 通知 OfficeManager，一个坏猫被回收了
            if (officeManager != null)
            {
                officeManager.BadCatSent();
            }
        }
    }
}