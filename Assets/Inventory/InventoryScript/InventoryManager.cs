using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public Inventory myBag;
    public GameObject slotGrid;
    public GameObject emptySlot;
    public TMP_Text itemInfo;
    public GameObject inventoryUI; 
    public GameObject shadowUI;
    public GameObject itemEffect; // 物品使用
    public TMP_Text unusableText;
    public float unusableTextTime=1f;

    public List<GameObject> slots=new List<GameObject>();

    public int selectedSlotIndex;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 动态绑定 itemEffectUI
        if (scene.name == "Village")
        {
            itemEffect = GameObject.Find("compass");
            if(instance.HasItem("蓝色线团")) itemEffect.SetActive(false);
        }
        else if (scene.name == "catchfish")
        {
            itemEffect = GameObject.Find("鱼叉");
        }
        else if(scene.name == "MiGong")
        {
            itemEffect = GameObject.Find("镰刀");
        }
        else if (scene.name == "kitchen")
        {
            itemEffect = GameObject.Find("Food");
        }
        else
        {
            itemEffect=null;
        }
        if(itemEffect!= null)
            instance.itemEffect.SetActive(false);

    }

    private void OnEnable()
    {
        if (instance == null) instance=this;
        if (instance.slotGrid != null)
            RefreshItem();
        instance.itemInfo.text = "";
        instance.unusableText.text = "";
        
        selectedSlotIndex = -1;
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance.itemInfo.text = itemDescription;   
    }

    public static void SelectSlot(int slotIndex)
    {
        instance.selectedSlotIndex = slotIndex;
    }

    public static void RefreshItem()
    {
        // 销毁所有物体
        for (int i = 0;i<instance.slotGrid.transform.childCount;i++)
        {
            if (instance.slotGrid.transform.childCount==0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }
        // 重新生成
        for(int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            //CreateNewItem(instance.myBag.itemList[i]);
            instance.slots.Add(Instantiate(instance.emptySlot));
            instance.slots[i]= Instantiate(instance.emptySlot, instance.slotGrid.transform);
            instance.slots[i].transform.SetParent(instance.slotGrid.transform, false);
            instance.slots[i].GetComponent<slot>().slotIndex = i;
            instance.slots[i].GetComponent<slot>().SetupSlot(instance.myBag.itemList[i]);
        }

    }

    // 获取物品
    public static item GetItemByName(string itemName)
    {
        return Resources.Load<item>("Items/" + itemName);
    }

    // 添加物品到背包
    public void AddItemToInventory(string itemName)
    {
        item newItem=GetItemByName(itemName);

        // 检查背包中是否已经该物体
        int existingIndex = -1;
        for (int i = 0; i < myBag.itemList.Count; i++)
        {
            if (myBag.itemList[i] != null && myBag.itemList[i].itemName == itemName)
            {
                existingIndex = i;
                break;
            }
        }

        if (existingIndex != -1)
        {
            // 已有，增加数量
            myBag.itemList[existingIndex].itemNum++;
        }
        else
        {
            // 没有，找到一个空位添加
            for (int i = 0; i < myBag.itemList.Count; i++)
            {
                if (myBag.itemList[i] == null)
                {
                    myBag.itemList[i] = newItem;
                    break;
                }
            }
        }

        // 刷新背包显示
        RefreshItem();
    }

    // 判断是否存在物品
    public bool HasItem(string itemName)
    {
        foreach (item item in myBag.itemList)
        {
            if (item != null && item.itemName == itemName)
            {
                return true;
            }
        }
        return false; 
    }

    // 使用物品
    public static void UseItem()
    {
        if (instance.selectedSlotIndex != -1 && instance.myBag.itemList[instance.selectedSlotIndex] != null)
        {
            
            item currentItem = instance.myBag.itemList[instance.selectedSlotIndex];
            if (currentItem.itemName == "罗盘" && SceneManager.GetActiveScene().name != "Village")
            {
                instance.unusableText.text = "此时无法使用罗盘。"; // 弹出提示
                instance.Invoke("StartFadeOut", instance.unusableTextTime);
                return;
            }
            if(currentItem.itemName == "白色线团"|| currentItem.itemName == "蓝色线团"|| currentItem.itemName == "黄色线团"|| currentItem.itemName == "红色线团"|| currentItem.itemName == "青色线团")
            {
                //if(SceneManager.GetActiveScene().name != "Village")
                //{
                    instance.unusableText.text = "此时无法使用线团。";
                    instance.Invoke("StartFadeOut", instance.unusableTextTime);
                    return;
                //}
            }
            if (currentItem.itemName == "鱼" || currentItem.itemName == "水稻" )
            {
                if (SceneManager.GetActiveScene().name != "kitchen")
                {
                    instance.unusableText.text = "此时无法使用食物。";
                    instance.Invoke("StartFadeOut", instance.unusableTextTime);
                    return;
                }
                if (currentItem.itemName == "鱼")
                {
                    instance.itemEffect.GetComponent<TMP_Text>().text = "已从背包中取出鱼";
                    FindObjectOfType<CookGameManager>().UpdateItemStatus("鱼");
                }
                else if (currentItem.itemName == "水稻")
                {
                    instance.itemEffect.GetComponent<TMP_Text>().text = "已从背包中取出水稻";
                    FindObjectOfType<CookGameManager>().UpdateItemStatus("水稻");
                }

                instance.itemEffect.SetActive(true);
                instance.Invoke("StartFadeOutItemEffectText", 2f);
            }
            if (currentItem.itemName == "镰刀" && SceneManager.GetActiveScene().name != "MiGong")
            {
                instance.unusableText.text = "此时无法使用镰刀。";
                instance.Invoke("StartFadeOut", instance.unusableTextTime);
                return;
            }
            if (currentItem.itemName == "鱼叉" && SceneManager.GetActiveScene().name != "catchfish")
            {
                instance.unusableText.text = "此时无法使用鱼叉。";
                instance.Invoke("StartFadeOut", instance.unusableTextTime);
                return;
            }
            if(instance.itemEffect!=null)
                instance.itemEffect.SetActive(true);

            instance.myBag.itemList[instance.selectedSlotIndex] = null;// 销毁背包中的物品
            
            instance.inventoryUI.SetActive(false);
            instance.shadowUI.SetActive(false);

            RefreshItem();
            instance.selectedSlotIndex = -1;
        }
    }

    private void StartFadeOut()
    {
        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        float duration = 0.5f; // 渐隐持续时间
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = 1f - (elapsedTime / duration);
            unusableText.color = new Color(unusableText.color.r, unusableText.color.g, unusableText.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        unusableText.text = ""; // 清空文本内容
        unusableText.color = new Color(unusableText.color.r, unusableText.color.g, unusableText.color.b, 1f); // 恢复初始透明度
    }

    private void StartFadeOutItemEffectText()
    {
        StartCoroutine(FadeOutItemEffectText());
    }

    private IEnumerator FadeOutItemEffectText()
    {
        TMP_Text effectText = instance.itemEffect.GetComponent<TMP_Text>();

        float duration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = 1f - (elapsedTime / duration);
            effectText.color = new Color(effectText.color.r, effectText.color.g, effectText.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        effectText.text = ""; 
        effectText.color = new Color(effectText.color.r, effectText.color.g, effectText.color.b, 1f); 

        instance.itemEffect.SetActive(false);
    }

}
