using UnityEngine;

public class RiceInteraction : MonoBehaviour
{
    private bool isCollected; // 新增收集状态标识

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true; // 立即标记为已收集
            GetComponent<Collider>().enabled = false; // 禁用碰撞体

            Debug.Log($"开始收集稻子：{name}");
            FarmGameManager.Instance?.CollectRice(transform.position);

            Destroy(gameObject, 0.1f); // 延迟销毁确保逻辑完成
        }
    }
}