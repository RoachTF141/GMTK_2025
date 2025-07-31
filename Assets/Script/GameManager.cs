using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int disappearCount = 0;
    public int targetCount = 4; // ��ɴ���

    public Image fadeOverlay;      // ����UI��Ļ
    public float fadeSpeed = 1f;   // �����ٶ�

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

        Debug.Log("������ɣ����Լ�����һ�ػ򴥷�ʤ��");
    }
}
