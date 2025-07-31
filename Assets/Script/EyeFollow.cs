using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    public float maxDistance = 0.1f;      // 最大偏移距离（单位：世界坐标）
    public float smoothSpeed = 10f;       // 跟随速度（越大越快）

    private Vector3 initialWorldPos;      // 眼珠初始位置（中心）
    private Vector3 targetPosition;       // 想要移动到的目标位置

    void Start()
    {
        initialWorldPos = transform.position;
        targetPosition = initialWorldPos;
    }

    void Update()
    {
        // 获取鼠标的世界坐标
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        // 计算方向 + 限制最大偏移
        Vector3 direction = mouseWorld - initialWorldPos;
        if (direction.magnitude > maxDistance)
        {
            direction = direction.normalized * maxDistance;
        }

        // 计算目标位置
        targetPosition = initialWorldPos + direction;

        // 平滑插值移动
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
    }
}
