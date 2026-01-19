using System.Collections.Generic;
using UnityEngine;

public class ArrowFactory : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Camera mainCamera;
    public int maxArrows = 10;
    private List<GameObject> arrows = new List<GameObject>();
    private int arrowsUsed = 0; // 新增：记录已使用的箭数量

    public GameObject GetArrow()
    {
        if (arrows.Count < maxArrows)
        {
            if (arrowPrefab != null)
            {
                // 计算向右的偏移量
                float offsetX = 4f;
                Vector3 spawnPosition = mainCamera.transform.position - mainCamera.transform.forward * 2 + mainCamera.transform.right * offsetX;
                GameObject arrow = Instantiate(arrowPrefab, spawnPosition, mainCamera.transform.rotation);
                arrows.Add(arrow);
                arrowsUsed++; // 新增：每次获取箭时增加计数
                return arrow;
            }
        }
        return null;
    }

    public void RemoveArrow(GameObject arrow)
    {
        if (arrows.Contains(arrow))
        {
            arrows.Remove(arrow);
        }
    }

    public void ResetArrows()
    {
        foreach (GameObject arrow in arrows)
        {
            if (arrow != null)
            {
                Destroy(arrow);
            }
        }
        arrows.Clear();
        arrowsUsed = 0; // 新增：重置时清零计数
    }

    // 新增方法：获取剩余箭数量
    public int GetRemainingArrows()
    {
        return maxArrows - arrowsUsed;
    }
}