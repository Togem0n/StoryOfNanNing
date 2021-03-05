using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// tooltips的UI组件background以及text长宽全都设置为0
/// Slot方法调用tooltips显示物品介绍
/// </summary>
public class Tooltips : MonoBehaviour
{

    private static Tooltips instance;

    [SerializeField]
    private Camera uiCamera;

    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
        transform.localPosition = localPoint;

    }

    private void showTooltips(string tooltipString)
    {
        gameObject.SetActive(true);
        textMeshPro.SetText(tooltipString);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        backgroundRectTransform.sizeDelta = textSize;
        Vector2 paddingSize = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize + paddingSize;
    }

    private void hideTooltips()
    {
        gameObject.SetActive(false);
    }

    public static void showTooltips_Static(string tooltipString)
    {
        instance.showTooltips(tooltipString);
    }

    public static void hideTooltips_Static()
    {
        instance.hideTooltips();
    }

}
