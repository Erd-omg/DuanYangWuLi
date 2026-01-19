using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate1 : MonoBehaviour
{
    //动画
    private float duration = 0.8f;//持续时间
    private bool isAnimating = false;
    private Vector3 newPosition = Vector3.zero;

    public GameObject shiwu11;
    public GameObject shiwu12;
    public GameObject shiwu13;

    void Update()
    {
        if (shiwu12.activeSelf && shiwu12.transform.localPosition != newPosition)
        {
            StartCoroutine(AnimateTransform(shiwu12, newPosition, 0f, duration));
        }
        if(shiwu13.activeSelf && shiwu13.transform.localPosition != newPosition)
        {
            StartCoroutine(AnimateTransform(shiwu13,newPosition,0f,duration));
        }
        if(shiwu12.transform.localPosition==newPosition && shiwu13.transform.localPosition == newPosition)
        {
            if(shiwu11.transform.localPosition != newPosition)
                StartCoroutine(AnimateTransform(shiwu11, newPosition, 90f, duration));
            else
                EndAnimation.instance.isOver = true;
        }
    }

    IEnumerator AnimateTransform(GameObject obj, Vector3 targetPosition, float Angle, float time)
    {
        if (isAnimating) yield break;
        isAnimating = true;

        Vector3 startPosition = obj.transform.localPosition;
        Quaternion startRotation = obj.transform.localRotation;
        Quaternion rotate= Quaternion.AngleAxis(Angle, Vector3.forward);
        Quaternion targetRotation = rotate * startRotation;

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / time;

            //平滑过渡到新位置、旋转
            obj.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, progress);
            obj.transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, progress);

            //等待下一帧
            yield return null;
        }

        obj.transform.localPosition = targetPosition;
        obj.transform.localRotation = targetRotation;

        Debug.Log(obj.name + "到位");

        isAnimating = false;
    }

    
}
