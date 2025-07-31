using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    public float maxDistance = 0.1f;      // ���ƫ�ƾ��루��λ���������꣩
    public float smoothSpeed = 10f;       // �����ٶȣ�Խ��Խ�죩

    private Vector3 initialWorldPos;      // �����ʼλ�ã����ģ�
    private Vector3 targetPosition;       // ��Ҫ�ƶ�����Ŀ��λ��

    void Start()
    {
        initialWorldPos = transform.position;
        targetPosition = initialWorldPos;
    }

    void Update()
    {
        // ��ȡ������������
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        // ���㷽�� + �������ƫ��
        Vector3 direction = mouseWorld - initialWorldPos;
        if (direction.magnitude > maxDistance)
        {
            direction = direction.normalized * maxDistance;
        }

        // ����Ŀ��λ��
        targetPosition = initialWorldPos + direction;

        // ƽ����ֵ�ƶ�
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
    }
}
