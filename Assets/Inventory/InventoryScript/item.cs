using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/New Item")]
public class item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemNum;
    [TextArea]
    public string itemInfo;

}
