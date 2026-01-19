using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    // 目标位置和目标大小
    public Vector3 newPosition = new Vector3(-125, 40, 300);
    public Vector3 newScale = new Vector3(1.6f, 1.6f, 1.6f);

    //动画
    private float duration = 0.5f;//持续时间
    private bool isAnimating = false;

    //记录物体的原参数
    private Vector3 oldPosition;
    private Vector3 oldScale;
    private Quaternion oldRotate;

    void Start()
    {
        oldPosition = transform.position;
        oldScale = transform.localScale;
        oldRotate = transform.rotation;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 将鼠标屏幕坐标转换为射线  
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 检查射线是否与物体相交  
            if (Physics.Raycast(ray, out hit))
                if (hit.collider.gameObject == gameObject)
                    StartCoroutine(AnimateTransform(newPosition, newScale, duration));
        }
    }

    IEnumerator AnimateTransform(Vector3 targetPosition, Vector3 targetScale, float time)
    {
        if (isAnimating) yield break;
        isAnimating = true;

        float elapsedTime = 0;
        Vector3 startPosition = transform.position;
        Vector3 startScale = transform.localScale;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / time;

            //平滑过渡到新位置和大小
            transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
            transform.localScale = Vector3.Lerp(startScale, targetScale, progress);

            //等待下一帧
            yield return null;
        }

        transform.position = targetPosition;
        transform.localScale = targetScale;
        isAnimating = false;
    }

    //保证目标位置只有一个物体
    private void OnTriggerEnter(Collider other)
    {
        if (transform.position == newPosition)
        {
            transform.position = oldPosition;
            transform.localScale = oldScale;
            transform.rotation = oldRotate;
        }
    }
}
