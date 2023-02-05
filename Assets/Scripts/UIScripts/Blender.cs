using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Blender : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public float blendTime;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts= true;
    }

    public void BlendIn(float delay)
    {
        StartCoroutine(BlendIntoScene(delay));
    }

    IEnumerator BlendIntoScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime / blendTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts= false;
    }

    public void BlendOut(float delay)
    {
        StartCoroutine(BlendSceneOut(delay));
    }

    IEnumerator BlendSceneOut(float delay)
    {
        yield return new WaitForSeconds(delay);
        canvasGroup.blocksRaycasts = true;
        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime / blendTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }
}
