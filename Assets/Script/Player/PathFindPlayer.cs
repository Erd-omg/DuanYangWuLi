using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFindPlayer : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;  // 途径点数组
    public Transform endTarget;    // 最终目标
    public float waypointRadius = 5f; // 到达途径点的判定半径

    // 动画相关
    public Animation animation;

    private int currentWaypointIndex = 0;
    private bool isFollowingFinalTarget = false;

    // 路径可视化相关
    public LineRenderer pathRenderer;
    public bool showPath = false;

    public GameObject pathEffectPrefab;
    private List<GameObject> pathEffects = new List<GameObject>(); // 存储路径特效实例

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animation = GetComponent<Animation>();
        pathRenderer = GetComponent<LineRenderer>();

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

        // 更新路径可视化
        if (showPath && agent.path != null)
        {
            UpdatePathVisualization();
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

    // 更新路径可视化
    private void UpdatePathVisualization()
    {
        if (agent.path == null || agent.path.corners.Length == 0)
        {
            pathRenderer.enabled = false;
            return;
        }

        pathRenderer.enabled = true;
        pathRenderer.positionCount = agent.path.corners.Length;
        pathRenderer.SetPositions(agent.path.corners);
    }

    // 切换路径可视化显示
    public void TogglePathVisualization(bool toggle)
    {
        showPath = toggle;
        if (!toggle)
        {
            pathRenderer.enabled = false;
        }
        else
        {
            UpdatePathVisualization();
        }
    }
}
