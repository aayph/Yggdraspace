using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BlendInOnStart : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public float delayedStart = 3f;
    public float blendTime = 5f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (delayedStart > 0f)
        {
            delayedStart -= Time.deltaTime;
            yield return null;
        }
        while (canvasGroup.alpha <= 1f)
        {
            canvasGroup.alpha += Time.deltaTime / blendTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }
}
