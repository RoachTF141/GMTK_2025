using UnityEngine;
using System.Collections;

public class OfficeManager : MonoBehaviour
{
    // 需要控制的UI元素引用
    public GameObject phonePanel4;
    public GameObject bossSaysOk; // 老板说行
    public GameObject bossTaskCatchCat; // 老板的任务抓猫
    public GameObject recyclingBinObject; // 回收站


    public GameObject[] badCats;
    private int unCaughtCatCount;

    void Start()
    {

        // 初始化坏猫数量
        if (badCats != null)
        {
            unCaughtCatCount = badCats.Length;
        }
    }

    // 用于外部调用的方法，以启动协程
    public void StartMailSequence()
    {
        StartCoroutine(MailTaskCompletedSequence());
    }


    public void BadCatSent()
    {
        unCaughtCatCount--;
        if (unCaughtCatCount <= 0)
        {
            Debug.Log("所有坏猫已被处理！");
            StartCoroutine(BadCatTaskCompletedSequence());
        }
    }



    // 邮件任务完成后的事件序列协程
    private IEnumerator MailTaskCompletedSequence()
    {
        Debug.Log("邮件任务完成，开始执行后续序列。");

        // 1. 隐藏电话面板4
        phonePanel4.SetActive(false);
        
        // 2. 启用老板说行
        bossSaysOk.SetActive(true);

        // 3. 等待2秒
        yield return new WaitForSeconds(2f);

        // 4. 隐藏老板说行
        bossSaysOk.SetActive(false);
        
        // 5. 启用老板的任务抓猫
        bossTaskCatchCat.SetActive(true);
        
        // 6. 启用回收站及其子对象
        recyclingBinObject.SetActive(true);

        Debug.Log("后续序列执行完毕。");
    }

    private IEnumerator BadCatTaskCompletedSequence()
    {
        Debug.Log("坏猫任务完成，开始执行后续序列。");
        bossTaskCatchCat.SetActive(false);
        bossSaysOk.SetActive(true);

        GameManager.Instance?.ReportItemClicked();
        GameManager.Instance?.ReportItemClicked();
        GameManager.Instance?.ReportItemClicked();
        GameManager.Instance?.ReportItemClicked();
        
        yield break;
    }
}