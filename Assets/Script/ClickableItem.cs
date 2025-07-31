using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class ClickableItem : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isClicked = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        if (isClicked) return;

        isClicked = true;

        // 播放音效
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }

        // 隐藏物体
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GameManager.Instance?.ReportItemClicked();

    }
}
