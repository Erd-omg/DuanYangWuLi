using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CloudManager : MonoBehaviour
{
    public static CloudManager Instance { get; private set; }

    public RectTransform cloudLeft;
    public RectTransform cloudRight;
    public Image whitePanel;

    public float animationDuration = 3.0f;
    public float originPosition = 1500.0f;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (!GetComponent<SceneLoader>())
            {
                gameObject.AddComponent<SceneLoader>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CloseClouds()
    {
        StartCoroutine(CloseCloudsCoroutine());
    }

    public void OpenClouds()
    {
        StartCoroutine(OpenCloudsCoroutine());
    }

    private IEnumerator CloseCloudsCoroutine()
    {
        float elapsedTime = 0;
        Vector3 initialLeftPos = cloudLeft.anchoredPosition;
        Vector3 initialRightPos = cloudRight.anchoredPosition;
        Color startColor = whitePanel.color;
        Color targetColor = new Color(1, 1, 1, 1);

        while (elapsedTime < animationDuration)
        {
            float t = Mathf.Clamp01(elapsedTime / animationDuration);
            cloudLeft.anchoredPosition = Vector3.Lerp(initialLeftPos, new Vector3(0, initialLeftPos.y), t);
            cloudRight.anchoredPosition = Vector3.Lerp(initialRightPos, new Vector3(0, initialRightPos.y), t);
            whitePanel.color = Color.Lerp(startColor, targetColor, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cloudLeft.anchoredPosition = new Vector3(0, cloudLeft.anchoredPosition.y);
        cloudRight.anchoredPosition = new Vector3(0, cloudRight.anchoredPosition.y);
        whitePanel.color = targetColor;
    }

    private IEnumerator OpenCloudsCoroutine()
    {
        float elapsedTime = 0;
        Vector3 finalLeftPos = new Vector3(-originPosition, cloudLeft.anchoredPosition.y);
        Vector3 finalRightPos = new Vector3(originPosition, cloudRight.anchoredPosition.y);
        Color startColor = whitePanel.color;
        Color targetColor = new Color(1, 1, 1, 0);

        while (elapsedTime < animationDuration)
        {
            float t = Mathf.Clamp01(elapsedTime / animationDuration);
            cloudLeft.anchoredPosition = Vector3.Lerp(cloudLeft.anchoredPosition, finalLeftPos, t);
            cloudRight.anchoredPosition = Vector3.Lerp(cloudRight.anchoredPosition, finalRightPos, t);
            whitePanel.color = Color.Lerp(startColor, targetColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cloudLeft.anchoredPosition = finalLeftPos;
        cloudRight.anchoredPosition = finalRightPos;
        whitePanel.color = targetColor;
    }
}