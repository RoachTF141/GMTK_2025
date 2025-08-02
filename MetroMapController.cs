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


    /// 处理拖动车的功能。
    void HandleCarDrag()
    {
        if (car == null) return;

        // 按下鼠标左键，开始拖动
        if (Input.GetMouseButtonDown(0) && IsMouseOver(car.GetComponent<Collider2D>()))
        {
            isDragging = true;
            offset = car.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // 鼠标左键抬起，结束拖动
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            // 结束拖动后检查是否到达目标位置
            CheckWinCondition();
        }

        // 鼠标左键按住期间，更新车的位置
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            // 只允许水平拖动（保持y轴不变）
            car.transform.position = new Vector3(newPosition.x, initialCarPosition.y, car.transform.position.z);
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