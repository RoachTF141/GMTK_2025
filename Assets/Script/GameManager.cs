using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int disappearCount = 0;
    public int targetCount = 4; // 达成次数

    public Image fadeOverlay;      // 拖入UI黑幕
    public float fadeSpeed = 1f;   // 渐变速度

    private bool fading = false;

    void Awake()
    {
        Instance = this;
    }

    public void ReportItemClicked()
    {
        disappearCount++;
        if (disappearCount >= targetCount && !fading)
        {
            fading = true;
            StartCoroutine(FadeToBlack());
        }
    }

    private System.Collections.IEnumerator FadeToBlack()
    {
        Color color = fadeOverlay.color;

        while (color.a < 1f)
        {
            color.a += Time.deltaTime * fadeSpeed;
            fadeOverlay.color = color;
            yield return null;
        }

        Debug.Log("场景完成，可以加载下一关或触发胜利");
    }
}
