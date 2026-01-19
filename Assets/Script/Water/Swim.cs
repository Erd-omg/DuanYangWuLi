using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour
{
    public float moveSpeed = 2.0f; // 鱼的移动速度
    public float changeDirectionInterval = 2.0f; // 改变方向的时间间隔
    public float rotationSpeed = 5.0f; // 鱼的旋转速度
    public float minDistanceToTerrain = 1.0f; // 鱼与地形的最小距离

    private Vector3 targetDirection; // 目标移动方向
    private float timeUntilChangeDirection; // 下次改变方向的时间

    void Start()
    {
        // 初始化随机方向
        ChangeDirection();
    }

    void Update()
    {
        // 移动鱼
        MoveFish();

        // 检查是否需要改变方向
        timeUntilChangeDirection -= Time.deltaTime;
        if (timeUntilChangeDirection <= 0)
        {
            ChangeDirection();
        }

        // 检测与地形的距离并调整方向
        CheckDistanceToTerrain();
    }

    void MoveFish()
    {
        // 向前移动
        transform.Translate(targetDirection * moveSpeed * Time.deltaTime, Space.World);

        // 平滑旋转到目标方向
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void ChangeDirection()
    {
        // 生成随机方向
        targetDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.2f, 0.2f), Random.Range(-1f, 1f)).normalized;

        // 重置时间间隔
        timeUntilChangeDirection = changeDirectionInterval;
    }

    void CheckDistanceToTerrain()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
        {
            if (hit.distance < minDistanceToTerrain)
            {
                // 如果距离小于最小距离，向上调整方向
                targetDirection = new Vector3(targetDirection.x, 0.5f, targetDirection.z).normalized;
            }
        }
    }
}