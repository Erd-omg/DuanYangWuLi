using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class itemOnDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Transform originParent;
    public Inventory myBag;
    private int currentItemIndex;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originParent=transform.parent;//slot
        currentItemIndex= originParent.GetComponent<slot>().slotIndex;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    { 
        // 交换物品
        if (eventData.pointerCurrentRaycast.gameObject.name == "Image")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
            transform.position=eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
            var temp = myBag.itemList[currentItemIndex];
            myBag.itemList[currentItemIndex] = myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<slot>().slotIndex];
            myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<slot>().slotIndex]=temp;

            eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originParent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originParent);

            GetComponent<CanvasGroup>().blocksRaycasts = true;//射线阻挡
            return;
        }
        else if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
        {
            // 移动到空格子
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<slot>().slotIndex] = myBag.itemList[currentItemIndex];
            if (eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<slot>().slotIndex != currentItemIndex)// 移动到原格子 
                myBag.itemList[currentItemIndex] = null;

            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        else// 若拖拽至背包外，归位
        {
            transform.SetParent(originParent);
            transform.position=originParent.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
