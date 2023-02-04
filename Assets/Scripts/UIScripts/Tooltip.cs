using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Device;

[RequireComponent(typeof(CanvasGroup))]
public class Tooltip : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public RectTransform panelTransform;
    public TextMeshProUGUI titleField;
    public TextMeshProUGUI textField;
    public ResourceOverview resOverview;

    private void Awake()
    {
        EventManager.TooltipAction += UpdateTooltip;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    private void OnDestroy()
    {
        EventManager.TooltipAction -= UpdateTooltip;
    }

    private void Update()
    {
        float x = Input.mousePosition.x + 30;
        float y = Input.mousePosition.y;

        if (Input.mousePosition.x > UnityEngine.Screen.width * 0.66f)
            x -= panelTransform.sizeDelta.x - 60;

        panelTransform.position = new Vector3(x, y, 0f);
    }

    void UpdateTooltip(string title, string text, Resources res)
    {
        if (string.IsNullOrEmpty(title))
        {
            canvasGroup.alpha = 0f;
            return;
        }

        titleField.text = title;
        textField.text = text;
        resOverview.UpdateNumbers(res);
        resOverview.gameObject.SetActive((res != null && res.HasValue()));
        canvasGroup.alpha = 1f;
    }
}