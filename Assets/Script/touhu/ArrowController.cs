using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float arrowSpeed = 50f;
    private Rigidbody rb;
    public Transform targetTransform; // 箭靶的 Transform

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("未找到 Rigidbody 组件！");
        }
    }

    public void Shoot()
    {
        if (targetTransform == null)
        {
            // 动态查找箭靶对象，假设箭靶对象的标签为 "Target"
            GameObject target = GameObject.FindWithTag("Target");
            if (target != null)
            {
                targetTransform = target.transform;
                Debug.Log("成功找到箭靶对象");
            }
            else
            {
                Debug.LogError("未找到带有 'Target' 标签的箭靶对象！");
                return;
            }
        }

        if (rb == null)
        {
            // 再次尝试获取 Rigidbody 组件
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody 组件为 null，无法发射箭！");
                return;
            }
        }

        // 确保刚体不是运动学模式
        if (rb.isKinematic)
        {
            rb.isKinematic = false;
            Debug.Log("将刚体的运动学模式关闭");
        }

        Vector3 direction;
        if (WindManager.currentWindSpeed == 0)
        {
            // 风速为零，必中中心
            if (targetTransform == null)
            {
                Debug.LogError("targetTransform 为 null，无法计算发射方向！");
                return;
            }
            direction = (targetTransform.position - transform.position).normalized;
        }
        else
        {
            // 其他风速，合理偏移
            if (targetTransform == null)
            {
                Debug.LogError("targetTransform 为 null，无法计算发射方向！");
                return;
            }
            direction = (targetTransform.position - transform.position).normalized;
            float offsetCoefficient = 0.01f;
            direction += Vector3.right * WindManager.currentWindSpeed * offsetCoefficient;
            direction = direction.normalized;
        }

        // 添加垂直方向的偏移，以调整发射角度
        float verticalOffset = 0f;
        direction += Vector3.up * verticalOffset;
        direction = direction.normalized;

        // 设置箭发射时水平旋转
        transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(90f, 0f, 0f);

        // 暂时关闭重力
        rb.useGravity = false;

        rb.velocity = direction * arrowSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target1") || other.CompareTag("Target2") || other.CompareTag("Target3") || other.CompareTag("Target4") || other.CompareTag("Target5"))
        {
            // 箭射中箭靶，将其固定在箭靶上
            rb.isKinematic = true;
            transform.SetParent(other.transform);
        }
    }

    private void EnableGravity()
    {
        rb.useGravity = true;
    }
}