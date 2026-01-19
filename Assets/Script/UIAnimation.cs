using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation: MonoBehaviour
{
    public float animationDuration = 0.5f; // 动画持续时间
    public Vector2 openSize ; // 打开时的大小
    public Vector2 closeSize = new Vector2(0.01f, 0.01f); // 关闭时的大小
    public GameObject uiPanel;

    private RectTransform rectTransform;
    private bool isOpen = false; // 当前是否打开
    private bool isAnimating = false; // 是否正在执行动画

    void Start()
    {
        rectTransform =uiPanel.GetComponent<RectTransform>();
        openSize = rectTransform.sizeDelta;
        
        rectTransform.sizeDelta = closeSize;
        uiPanel.SetActive(false);
    }

    // 调用此方法来切换 UI 的显示和隐藏
    public void ToggleUI()
    {
        if (isAnimating) return; 
        isAnimating = true;

        if (isOpen)
        {
            StartCoroutine(CloseUI());
        }
        else
        {
            StartCoroutine(OpenUI());
        }
    }

    // 打开 UI 的动画
    private IEnumerator OpenUI()
    {
        isOpen = true;
        uiPanel.SetActive(true);

        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            rectTransform.sizeDelta = Vector2.Lerp(closeSize, openSize, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.sizeDelta = openSize;
        isAnimating = false;
    }

    // 关闭 UI 的动画
    private IEnumerator CloseUI()
    {
        isOpen = false;

        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            rectTransform.sizeDelta = Vector2.Lerp(openSize, closeSize, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.sizeDelta = closeSize;
        uiPanel.SetActive(false);
        isAnimating = false;
    }
}
