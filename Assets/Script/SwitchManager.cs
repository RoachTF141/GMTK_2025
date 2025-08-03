using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    // 需要在Inspector中关联开关和灯的GameObject
    public GameObject lightObject; // day1-4light
    public GameObject switchObject; // day1-4switch

    public BoxCollider2D blanketCollider; // IMG_0930_Blanket的Box Collider 2D

    void Start()
    {
        // 确保一开始毯子的Box Collider 2D是禁用的
        if (blanketCollider != null)
        {
            blanketCollider.enabled = false;
        }
    }


    // 当点击开关时调用的函数
    void OnMouseDown()
    {
        Debug.Log("开关被点击了");
        // 隐藏灯光对象
        if (lightObject != null)
        {
            lightObject.SetActive(false);
        }

        // 当灯光隐藏后，启用毯子的Box Collider 2D
        if (blanketCollider != null)
        {
            blanketCollider.enabled = true;
        }

    }
}