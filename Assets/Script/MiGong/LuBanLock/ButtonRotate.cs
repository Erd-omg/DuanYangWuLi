using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRotate : MonoBehaviour
{
    private GameObject selectedPart;

    private float angle = 90f;//旋转角度
    private float duration = 0.25f;//旋转速度

    private bool isRotating=false;

    private void Update()
    {
        // 向前方发射射线 
        Ray ray = new Ray(new Vector3(-125, 45, 0), Vector3.forward);  
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500f))   
        {
            selectedPart=hit.collider.gameObject;
        }
        else
        {
            selectedPart = null;   
        }
    }

    public void upButton()
    {
        StartCoroutine(AnimateRotate(angle,Vector3.right,duration));
    }

    public void downButton()
    {
        StartCoroutine(AnimateRotate(angle, Vector3.left, duration));
    }

    public void leftButton() 
    {
        StartCoroutine(AnimateRotate(angle, Vector3.up, duration));
    }

    public void rightButton()
    {
        StartCoroutine(AnimateRotate(angle, Vector3.down, duration));
    }

    IEnumerator AnimateRotate(float Angle, Vector3 Axis, float time)
    {
        if(selectedPart==null) yield break;
        if(isRotating) yield break;
        isRotating=true;

        float elapsedTime = 0;
        Quaternion startRotate = selectedPart.transform.rotation;//初始旋转
        Quaternion rotate = Quaternion.AngleAxis(Angle, Axis);
        Quaternion targetRotation=rotate*startRotate;//目标旋转

        while (elapsedTime < time + Time.deltaTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / time);

            //平滑过渡到新旋转
            selectedPart.transform.rotation = Quaternion.Lerp(startRotate, targetRotation, progress);

            //等待下一帧
            yield return null;
        }

        selectedPart.transform.rotation = targetRotation;
        isRotating = false;

    }
}
