using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerArrow : MonoBehaviour
{
    public int scoreToWin = 60;
    public TMP_Text scoreText;
    public TMP_Text windInfoText;
    public TMP_Text timerText;
    public TMP_Text ArrowNum;
    public ArrowFactory arrowFactory;
    public GameObject gameOverPanel;
    public GameObject winPanel;

    private int score;
    private bool isGameOver = false;
    private float timer = 60f;

    void Start()
    {
        score = 0;
        UpdateScoreText();
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        UpdateArrowCount();
    }

    void Update()
    {
        if (!isGameOver)
        {
            UpdateTimer();
            UpdateWindInfoText();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject arrow = arrowFactory.GetArrow();
                if (arrow != null)
                {
                    ArrowController arrowController = arrow.GetComponent<ArrowController>();
                    if (arrowController != null)
                    {
                        arrowController.Shoot();
                        UpdateArrowCount();
                    }
                }
            }

            if (timer <= 0)
            {
                EndGame();
            }
        }
    }

    private void UpdateArrowCount()
    {
        if (arrowFactory != null && ArrowNum != null)
        {
            ArrowNum.text = "剩余箭数: " + arrowFactory.GetRemainingArrows();
        }
        if (arrowFactory.GetRemainingArrows() <= 0 && !isGameOver)
        {
            EndGame();
        }
    }

    public void AddScore(string targetTag)
    {
        int points = 0;
        switch (targetTag)
        {
            case "Target1":
                points = 20;
                break;
            case "Target2":
                points = 15;
                break;
            case "Target3":
                points = 10;
                break;
            case "Target4":
                points = 10;
                break;
            case "Target5":
                points = 5;
                break;
        }

        score += points;
        UpdateScoreText();

        if (score >= scoreToWin)
        {
            WinGame();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "当前分数: " + score;
    }

    private void UpdateWindInfoText()
    {
        windInfoText.text = "风速: " + WindManager.currentWindSpeed.ToString("F2") + " 方向: " + WindManager.windDirection;
    }

    private void UpdateTimer()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 0;
        }
        timerText.text = "  " + Mathf.FloorToInt(timer);
    }

    private void WinGame()
    {
        isGameOver = true;
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void EndGame()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResetGame()
    {
        score = 0;
        timer = 60f;
        isGameOver = false;
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        arrowFactory.ResetArrows();
        UpdateScoreText();
        UpdateTimer();
        UpdateArrowCount();
    }
}