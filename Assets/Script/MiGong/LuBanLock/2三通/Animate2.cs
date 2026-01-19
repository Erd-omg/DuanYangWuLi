using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate2 : MonoBehaviour
{
    //动画
    private float duration = 0.8f;//持续时间
    private bool isAnimating = false;
    private Vector3 newPosition = Vector3.zero;

    public GameObject shiwu21;
    public GameObject shiwu22;
    public GameObject shiwu23;

    void Update()
    {
        if (shiwu22.activeSelf && shiwu22.transform.localPosition != newPosition)
        {
            StartCoroutine(AnimateTransform(shiwu22, newPosition, duration));
        }
        if (shiwu23.activeSelf && shiwu23.transform.localPosition != newPosition)
        {
            StartCoroutine(AnimateTransform(shiwu23, newPosition,duration));
        }
        if (shiwu22.transform.localPosition == newPosition && shiwu23.transform.localPosition == newPosition)
        {
            if (shiwu21.transform.localPosition != newPosition)
                StartCoroutine(AnimateTransform(shiwu21, newPosition,duration));
            else
                EndAnimation.instance.isOver = true;
        }
    }

    IEnumerator AnimateTransform(GameObject obj, Vector3 targetPosition, float time)
    {
        if (isAnimating) yield break;
        isAnimating = true;

        Vector3 startPosition = obj.transform.localPosition;

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / time;

            //平滑过渡到新位置
            obj.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, progress);

            //等待下一帧
            yield return null;
        }

        obj.transform.localPosition = targetPosition;

        Debug.Log(obj.name + "到位");

        isAnimating = false;
    }
}
