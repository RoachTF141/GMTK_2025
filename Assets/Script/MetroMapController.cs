using UnityEngine;
using System.Collections;

public class MetroMapController : MonoBehaviour
{
    // 任务所需的公共变量
    public GameObject poster;
    public GameObject metroMapParent; // 包含地图before, 地图after和车的父物体
    public GameObject car;
    public GameObject mapAfter;
    public GameObject mapBefore;
    public Vector3 targetPosition; // 目标位置（23街）
    public float winThreshold = 0.5f; // 停车位置的容差
    public AudioClip winSound; // 过关音效

    public GameObject day1_2Carriage;//车厢门

    private AudioSource audioSource;
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 initialCarPosition;

    // 任务完成标志
    private bool taskCompleted = false;

    void Start()
    {
        // 游戏开始时隐藏MetroMap及其子物体
        if (metroMapParent != null)
        {
            metroMapParent.SetActive(false);
        }

        // 获取AudioSource组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 存储车的初始位置
        if (car != null)
        {
            initialCarPosition = car.transform.position;
        }

        // 确保mapAfter一开始是隐藏的
        if (mapAfter != null)
        {
            mapAfter.SetActive(false);
        }
    }

    void Update()
    {
        // 只有在任务未完成时才处理点击和拖动
        if (taskCompleted) return;

        // 如果MetroMap是隐藏的，处理海报点击
        if (!metroMapParent.activeSelf)
        {
            HandlePosterClick();
        }
        else // 如果MetroMap是显示的，处理拖动和关闭
        {
            HandleCarDrag();
            HandleMapClose();
        }
    }


    /// 处理海报点击事件，显示地图。
    void HandlePosterClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == poster)
            {
                metroMapParent.SetActive(true);
            }
        }
    }

    //处理拖车问题
    void HandleCarDrag()
    {
        if (car == null)
        {
            Debug.LogWarning("Car object is not assigned or is null.");
            return;
        }

        // 按下鼠标左键，开始拖动
        if (Input.GetMouseButtonDown(0))
        {
            // 检查鼠标是否在任何2D Collider上
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null)
            {
                // 打印被点击的物体名称，这非常关键
                Debug.Log("Mouse clicked on object: " + hit.collider.gameObject.name);

                // 判断被点击的物体是否是“车”
                if (hit.collider.gameObject == car)
                {
                    Debug.Log("Mouse clicked on the car! Starting drag.");
                    isDragging = true;
                    offset = car.transform.position - mouseWorldPos;
                }
                else
                {
                    Debug.Log("Mouse clicked on something else, not the car.");
                }
            }
            else
            {
                Debug.Log("Mouse clicked on empty space.");
            }
        }

        // 鼠标左键抬起，结束拖动
        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                Debug.Log("Mouse button released, ending drag.");
                isDragging = false;
                CheckWinCondition();
            }
        }

        // 鼠标左键按住期间，更新车的位置
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            // 只允许水平拖动（保持y轴不变）
            car.transform.position = new Vector3(newPosition.x, initialCarPosition.y, car.transform.position.z);
            // 持续打印车的位置，确保它正在移动
            Debug.Log("Car is being dragged to position: " + car.transform.position);
        }
    }

    /// 检测鼠标是否在指定的Collider2D上。
    bool IsMouseOver(Collider2D collider)
    {
        if (collider == null) return false;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return collider.bounds.Contains(mouseWorldPos);
    }


    /// 检查是否满足过关条件。
    void CheckWinCondition()
    {
        if (car == null) return;

        // 检查车是否在目标位置附近
        if (Vector3.Distance(new Vector3(car.transform.position.x, 0, 0), new Vector3(targetPosition.x, 0, 0)) < winThreshold)
        {
            taskCompleted = true;
            Debug.Log("任务完成！");
            mapBefore.SetActive(false);
            mapAfter.SetActive(true);
            
            // 播放过关音效
            if (audioSource != null && winSound != null)
            {
                audioSource.PlayOneShot(winSound);
            }
            
            // 播放小过关音效后，延迟一小段时间再执行后续操作
            StartCoroutine(CompleteTaskAfterSound());
        }
    }

    IEnumerator CompleteTaskAfterSound()
    {

        yield return new WaitForSeconds(winSound.length);


        if (mapAfter != null)
        {
            mapBefore.SetActive(false);
            mapAfter.SetActive(true);
        }
        
        if (car != null) Destroy(car);
        mapAfter.SetActive(false);

        // 移动地铁车厢
    if (day1_2Carriage != null)
    {
        // 获取车厢的初始位置
        Vector3 carriageStartPosition = day1_2Carriage.transform.position;
        // 计算屏幕左侧的目标位置 (可以根据你的场景调整)
        Vector3 carriageEndPosition = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f));
        // 保持车厢的Y轴和Z轴不变
        carriageEndPosition.y = carriageStartPosition.y;
        carriageEndPosition.z = carriageStartPosition.z;

        float moveDuration = 2f; // 移动持续时间，可以调整
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            // 使用Lerp平滑移动
            day1_2Carriage.transform.position = Vector3.Lerp(carriageStartPosition, carriageEndPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保移动到目标位置
        day1_2Carriage.transform.position = carriageEndPosition;
    }
    else
    {
        Debug.LogWarning("day1-2carriage GameObject is not assigned in the Inspector.");
    }
        GameManager.Instance?.ReportItemClicked();
        GameManager.Instance?.ReportItemClicked();
        GameManager.Instance?.ReportItemClicked();
        GameManager.Instance?.ReportItemClicked();
    }


    /// 点击非地图区域，关闭地铁图。
    void HandleMapClose()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            // 如果点击的物体是空，或者点击的物体不是地铁地图父物体及其子物体，则隐藏地铁图
            if (hit.collider == null || (hit.collider.gameObject != poster && !IsParentOrChildOf(hit.collider.gameObject, metroMapParent)))
            {
                metroMapParent.SetActive(false);
            }
        }
    }

    /// 检查一个游戏物体是否是另一个游戏物体的子物体或本身。
    bool IsParentOrChildOf(GameObject child, GameObject parent)
    {
        if (child == null || parent == null) return false;
        if (child == parent) return true;
        
        Transform current = child.transform;
        while (current != null)
        {
            if (current.gameObject == parent)
            {
                return true;
            }
            current = current.parent;
        }
        return false;
    }
}