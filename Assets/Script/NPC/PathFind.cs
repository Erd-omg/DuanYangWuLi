using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFind : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;  // 途径点数组
    public Transform endTarget;    // 最终目标
    public float waypointRadius = 0.5f; // 到达途径点的判定半径

    // 动画相关
    private Animation animation;

    private int currentWaypointIndex = 0;
    private bool isFollowingFinalTarget = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animation = GetComponent<Animation>();
        // 如果有途径点，先前往第一个途径点
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void Update()
    {
        if (!isFollowingFinalTarget)
        {
            // 检查是否到达当前途径点
            if (HasReachedDestination())
            {
                MoveToNextWaypoint();
            }
        }
        else
        {
            // 持续跟随最终目标
            agent.SetDestination(endTarget.position);
        }
    }

    void OnEnable()
    {
        // 重新初始化导航状态
        agent = GetComponent<NavMeshAgent>();
        animation = GetComponent<Animation>();
        currentWaypointIndex = 0;
        isFollowingFinalTarget = false;

        // 重置动画（如果需要）
        if (animation != null)
        {
            animation.Play();
        }

        // 如果有途径点则前往第一个，否则直接前往终点
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
        else if (endTarget != null)
        {
            agent.SetDestination(endTarget.position);
        }
    }
    bool HasReachedDestination()
    {
        // 当没有正在计算的路径且剩余距离小于判定半径时
        return !agent.pathPending
               && agent.remainingDistance <= waypointRadius;
    }

    void MoveToNextWaypoint()
    {
        if (currentWaypointIndex < waypoints.Length - 1)
        {
            // 前往下一个途径点
            currentWaypointIndex++;
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
        else
        {
            // 所有途径点走完，开始跟随最终目标
            isFollowingFinalTarget = true;
            agent.SetDestination(endTarget.position);
        }
    }

    // 可在运行时调用的方法：设置新路径
    public void SetNewPath(Transform[] newWaypoints, Transform newEndTarget)
    {
        waypoints = newWaypoints;
        endTarget = newEndTarget;
        currentWaypointIndex = 0;
        isFollowingFinalTarget = false;

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
        else
        {
            agent.SetDestination(endTarget.position);
        }
    }
}
