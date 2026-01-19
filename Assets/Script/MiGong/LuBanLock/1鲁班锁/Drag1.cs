using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag1 : MonoBehaviour
{
    //拖拽
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isDragging = false;

    //原位置
    private Vector3 targetPosition = new Vector3(-125, 40, 300);

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 将鼠标屏幕坐标转换为射线  
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 检查射线是否与物体相交  
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // 记录开始拖拽时的屏幕点  
                    screenPoint = Camera.main.WorldToScreenPoint(hit.point);
                    offset = hit.point - transform.position;
                    isDragging = true;
                }
            }
        }

        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            Vector3 newPosition = new Vector3(curPosition.x, curPosition.y, 300f) + offset;

            //设置物体的位置
            transform.position = newPosition;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            //实物显示，提示消失，透明消失，零件消失
            if (triggerCheck1_1.instance.IsEnterTrans)
            {
                triggerCheck1_1.instance.shiwu.SetActive(true);
                triggerCheck1_1.instance.transparency.SetActive(false);
                triggerCheck1_1.instance.right.SetActive(false);
                triggerCheck1_1.instance.wrong.SetActive(false);
                triggerCheck1_1.instance.part.SetActive(false);
            }
            else
            {
                transform.position = targetPosition;
            }

            if (triggerCheck1_2.instance.IsEnterTrans)
            {
                triggerCheck1_2.instance.shiwu.SetActive(true);
                triggerCheck1_2.instance.transparency.SetActive(false);
                triggerCheck1_2.instance.right.SetActive(false);
                triggerCheck1_2.instance.wrong.SetActive(false);
                triggerCheck1_2.instance.part.SetActive(false);
            }
            else
            {
                transform.position = targetPosition;
            }

            if (triggerCheck1_3.instance.IsEnterTrans)
            {
                triggerCheck1_3.instance.shiwu.SetActive(true);
                triggerCheck1_3.instance.transparency.SetActive(false);
                triggerCheck1_3.instance.right.SetActive(false);
                triggerCheck1_3.instance.wrong.SetActive(false);
                triggerCheck1_3.instance.part.SetActive(false);
            }
            else
            {
                transform.position = targetPosition;
            }

        }
    }
}
