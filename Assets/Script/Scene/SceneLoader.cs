using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public string targetSceneName; // 目标场景名称

    private bool isAnimating = false;

    // 开始场景切换
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        if (isAnimating) yield break;

        isAnimating = true;

        // 合上动画
        CloudManager.Instance.CloseClouds();
        yield return new WaitForSeconds(CloudManager.Instance.animationDuration);

        // 异步加载新场景
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // 等待场景加载完成
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 打开动画
        CloudManager.Instance.OpenClouds();
        yield return new WaitForSeconds(CloudManager.Instance.animationDuration);

        isAnimating = false;
    }
}
