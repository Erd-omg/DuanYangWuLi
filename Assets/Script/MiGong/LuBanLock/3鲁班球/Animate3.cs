using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate3 : MonoBehaviour
{
    //动画
    private float duration = 0.8f;//持续时间
    private bool isAnimating = false;
    private Vector3 newPosition = Vector3.zero;
    private Vector3 Position32 = new Vector3(36f,0f,-18f);

    private bool isMoved = false;
    private bool isRotated=false;

    public GameObject shiwu31;
    public GameObject shiwu32;
    public GameObject shiwu33;
    public GameObject shiwu34;
    public GameObject shiwu35;
    public GameObject shiwu36;

    void Update()
    {
        //shiwu32的拼装动画
        if (shiwu32.activeSelf)
        {
            if(shiwu32.transform.localPosition != newPosition && !isMoved)
                StartCoroutine(AnimateTransform(shiwu32, newPosition, 0f, duration));
            else if(shiwu32.transform.localPosition == newPosition)
                isMoved = true;
        }
        //shiwu33的拼装动画
        if (shiwu33.activeSelf && shiwu33.transform.localPosition != newPosition)
        {
            StartCoroutine(AnimateTransform(shiwu33, newPosition, 0f, duration));
        }
        //shiwu34的拼装动画
        if (shiwu34.activeSelf && shiwu34.transform.localPosition != newPosition)
        {
            StartCoroutine(AnimateTransform(shiwu34, newPosition, 0f, duration));
        }
        //shiwu32的中途旋转动画
        if (shiwu34.transform.localPosition == newPosition)
        {
            if (shiwu32.transform.localPosition != Position32 && !isRotated)
                StartCoroutine(AnimateTransform(shiwu32, Position32, 90f, duration));
            else if (shiwu32.transform.localPosition == Position32)
                isRotated = true;
        }
        //shiwu35的拼装动画
        if (shiwu35.activeSelf && shiwu35.transform.localPosition != newPosition)
        {
            StartCoroutine(AnimateTransform(shiwu35, newPosition, 0f, duration));
        }
        //shiwu36的拼装动画
        if (shiwu36.activeSelf && shiwu36.transform.localPosition != newPosition)
        {
            StartCoroutine(AnimateTransform(shiwu36, newPosition, 0f, duration));
        }
        //shiwu32的结尾旋转动画
        if (shiwu36.transform.localPosition == newPosition)
        {
            if (shiwu32.transform.localPosition != newPosition)
                StartCoroutine(AnimateTransform(shiwu32, newPosition, -90f, duration));
            else
                EndAnimation.instance.isOver = true;
        }

        //if (shiwu12.transform.localPosition == newPosition && shiwu13.transform.localPosition == newPosition)
        //{
        //    if (shiwu11.transform.localPosition != newPosition)
        //        StartCoroutine(AnimateTransform(shiwu11, newPosition, 90f, duration));
        //    else
        //        EndAnimation.instance.isOver = true;
        //}
    }

    IEnumerator AnimateTransform(GameObject obj, Vector3 targetPosition, float Angle, float time)
    {
        if (isAnimating) yield break;
        isAnimating = true;

        Vector3 startPosition = obj.transform.localPosition;
        Quaternion startRotation = obj.transform.localRotation;
        Quaternion rotate = Quaternion.AngleAxis(Angle, Vector3.down);
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
