using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Button myButton;

    void Start()
    {
        // 获取Button组件
        myButton = GetComponent<Button>();

        // 添加点击事件监听
        myButton.onClick.AddListener(HideButton);
    }

    // 点击后隐藏按钮的方法
    public void HideButton()
    {
        // 将按钮自身设为false（隐藏）
        gameObject.SetActive(false);

        // 如果需要同时隐藏父对象，可以使用：
        // transform.parent.gameObject.SetActive(false);
    }
}