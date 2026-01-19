using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimation : MonoBehaviour
{
    //动画
    private float duration = 3f;//持续时间
    private float rotationSpeed = 60f;//自转速度
    public Vector3 newPosition = new Vector3(0, 30, 300);
    public Vector3 midPosition = new Vector3(0, 0, 300);
    public Vector3 newScale = new Vector3(1.3f, 1.3f, 1.3f);
    public Vector3 midScale = new Vector3(1.5f, 1.5f, 1.5f);
    private bool isAnimating = false;

    //关闭mouseRotate组件
    public MouseRotate mouseRotate;

    //判断是否组装完成
    public bool isOver=false;
    public static EndAnimation instance;

    //避免重复进入协程
    private bool isFinished = false;

    //进入下一关
    public GameObject panel;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (isOver && !isFinished)
        {
            StartCoroutine(waitFourSeconds());
            isFinished = true;
        }
    }

    //动画
    IEnumerator AnimateTransform(Vector3 targetPosition,Vector3 midPosition, Vector3 targetScale, Vector3 midScale, float time)
    {
        if (isAnimating) yield break;
        isAnimating = true;

        float elapsedTime = 0;
        Vector3 startPosition = transform.position;
        Vector3 startScale = transform.localScale;

        //放大阶段
        while (elapsedTime < time / 2f)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / (time / 2f);

            //平滑过渡到中间位置和中间大小
            transform.position = Vector3.Lerp(startPosition, midPosition, progress);
            transform.localScale = Vector3.Lerp(startScale, midScale, progress);

            //自转
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);

            //等待下一帧
            yield return null;
        }

        //缩小阶段
        elapsedTime = 0;//重置计时器
        while (elapsedTime < time / 2f)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / (time / 2f);

            //平滑过渡到新位置和新大小
            transform.position = Vector3.Lerp(midPosition, targetPosition, progress);
            transform.localScale = Vector3.Lerp(midScale, targetScale, progress);

            //自转
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);

            //等待下一帧
            yield return null;
        }

        transform.position = targetPosition;
        transform.localScale = targetScale;

        if(panel!=null) 
            panel.SetActive(true);

        isAnimating = false;
    }

    //等待
    IEnumerator waitFourSeconds()
    {
        yield return new WaitForSeconds(0.8f);
        mouseRotate.enabled=false;
        StartCoroutine(AnimateTransform(newPosition,midPosition, newScale, midScale, duration));
    }
}
