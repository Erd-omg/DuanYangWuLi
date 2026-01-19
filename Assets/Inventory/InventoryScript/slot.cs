using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class slot : MonoBehaviour
{
    public item thisItem;
    public Image thisImage;
    public TMP_Text thisNum;
    public string thisInfo;

    public GameObject itemInSlot;

    public int slotIndex;// ŒÔ∆∑ID

    public void ItemOnClick()
    {
        InventoryManager.UpdateItemInfo(thisInfo);
        InventoryManager.SelectSlot(slotIndex);
    }

    public void SetupSlot(item item)
    {
        if(item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        thisImage.sprite=item.itemImage;
        thisNum.text=item.itemNum.ToString();
        thisInfo=item.itemInfo.ToString();
    }
}
