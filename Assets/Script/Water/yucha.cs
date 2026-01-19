using UnityEngine;

public class yucha : MonoBehaviour
{
    public float moveSpeed = 10.0f; // 鱼叉移动速度
    public GameObject fishParticleEffect; // 鱼的粒子效果
    public float overlapRadius = 50.0f; // 鼠标与鱼的屏幕重叠半径（像素）

    void Update()
    {
        // 获取鼠标在屏幕上的位置
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10; // 设置与摄像机的距离

        // 将屏幕坐标转换为世界坐标
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 移动鱼叉到鼠标位置
        transform.position = Vector3.Lerp(transform.position, worldPosition, moveSpeed * Time.deltaTime);

        // 检测鼠标点击
        if (Input.GetMouseButtonDown(0))
        {
            
            // 遍历所有鱼，检查是否与鼠标位置重叠
            GameObject[] fishList = GameObject.FindGameObjectsWithTag("Fish");
            foreach (GameObject fish in fishList)
            {
                if (fish != null)
                {
                    // 将鱼的世界坐标转换为屏幕坐标
                    Vector3 fishScreenPos = Camera.main.WorldToScreenPoint(fish.transform.position);

                    // 检查鼠标位置与鱼的屏幕位置是否重叠
                    if (Vector3.Distance(mousePosition, fishScreenPos) <= overlapRadius)
                    {
                        // 触发鱼的消除或粒子化效果
                        Destroy(fish);
                        Instantiate(fishParticleEffect, fish.transform.position, Quaternion.identity);

                        // 更新游戏状态
                        GameManager.Instance.CatchFish();
                        break; // 每次点击只处理一条鱼
                    }
                }
            }
        }
    }
}