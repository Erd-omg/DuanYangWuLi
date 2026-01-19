using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneRenew : MonoBehaviour
{
    private GameManagerArrow gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManagerArrow>();
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(RestartCurrentScene);
        }
    }

    void RestartCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        if (gameManager != null)
        {
            gameManager.ResetGame();
        }
    }
}
