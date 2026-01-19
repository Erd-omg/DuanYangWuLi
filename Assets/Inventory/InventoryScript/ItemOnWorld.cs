using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public item thisItem;
    public Inventory playerInventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            addNewItem();
            Destroy(gameObject);
        }
    }

    public void addNewItem()
    {
        if(!playerInventory.itemList.Contains(thisItem))
        { 
            //playerInventory.itemList.Add(thisItem);
            for(int i = 0; i < playerInventory.itemList.Count; i++)
            {
                if (playerInventory.itemList[i] == null)
                {
                    playerInventory.itemList[i]=thisItem;
                    break;
                }
            }
        }
        else
        {
            thisItem.itemNum++;
        }
        InventoryManager.RefreshItem();
    }
}
