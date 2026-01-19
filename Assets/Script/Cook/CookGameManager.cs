using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CookGameManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject[] stepButtons;
    public GameObject interactionPanel;
    public Slider progressSlider;
    public TMP_Text completionText;
    public TMP_Text instructionText;
    public GameObject finalWinText;
    public GameObject successPanel;
    public TMP_Text Tip;
    public GameObject food;

    [Header("Game Settings")]
    [Range(0.1f, 2f)] public float progressSpeed = 0.5f;
    [SerializeField] private float minSuccessThreshold = 0.6f;
    [SerializeField] private float maxSuccessThreshold = 0.8f;

    private int currentStep = 0;
    private bool isInteracting = false;
    private bool hasPressedSpace = false;

    private Image progressFillImage;

    private Color originColor = new Color(0, 0, 0, 1);
    private Color unusableColor = new Color(0.52f, 0, 0, 1);

    private bool isFishTaken = false;
    private bool isRiceTaken = false;

    private bool isGaming = false;

    void Start()
    {
        InitializeGame();
        progressFillImage = progressSlider.fillRect.GetComponent<Image>();
    }

    private void Update()
    {
        if (!isFishTaken || !isRiceTaken)
        {
            Tip.text = "请从背包中取出鱼和水稻！";
            Tip.color = unusableColor;
        }
        else if (!isGaming && !isInteracting)
        {
            isGaming = true;
            stepButtons[0].SetActive(true);
            Tip.text = "请开始" + GetStepAction(currentStep) + "…";
            Tip.color = originColor;
        }
    }

    void InitializeGame()
    {
        foreach (var button in stepButtons)
            button.SetActive(false);

        interactionPanel.SetActive(false);
        completionText.gameObject.SetActive(false);
        finalWinText.SetActive(false);
        progressSlider.value = 0;
        hasPressedSpace = false;
        isInteracting = false;

        isFishTaken = false;
        isRiceTaken = false;
        isGaming = false;
    }

    public void StartInteraction(int stepIndex)
    {
        // 如果请求的步骤不是当前步骤且不是下一步骤，则忽略
        //if (stepIndex != currentStep && stepIndex != currentStep + 1) return;
        if (stepIndex != currentStep || isInteracting) return;
        // 如果是下一步骤且不在交互中，则更新当前步骤
        if (stepIndex == currentStep + 1 && !isInteracting)
        {
            currentStep = stepIndex;
            progressSlider.value = 0;
            UpdateButtonVisibility();
            return;
        }

        if (isInteracting) return;

        Tip.text = "请开始" + GetStepAction(currentStep) + "…";
        Tip.color = originColor;

        StartCoroutine(InteractionProcess());
    }

    IEnumerator InteractionProcess()
    {
        isInteracting = true;
        hasPressedSpace = false;

        stepButtons[currentStep].SetActive(false);
        interactionPanel.SetActive(true);
        progressSlider.value = 0;
        instructionText.text = GetInstructionText(currentStep);

        completionText.gameObject.SetActive(false);

        Tip.text = "正在" + GetStepAction(currentStep) + "中…";
        Tip.color = originColor;

        while (isInteracting)
        {
            if (Input.GetKey(KeyCode.Space) && !hasPressedSpace)
            {
                hasPressedSpace = true;
                while (Input.GetKey(KeyCode.Space))
                {
                    progressSlider.value += Time.deltaTime * progressSpeed;

                    // 根据进度计算颜色
                    Color newColor = Color.Lerp(Color.white, Color.red, progressSlider.value);
                    progressFillImage.color = newColor;

                    if (progressSlider.value >= 1f)
                    {
                        progressSlider.value = 1f;
                        yield return CheckProgressResult();
                        yield break;
                    }
                    yield return null;
                }
                yield return CheckProgressResult();
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator CheckProgressResult()
    {
        bool isSuccess = progressSlider.value >= minSuccessThreshold &&
                         progressSlider.value <= maxSuccessThreshold;

        completionText.text = isSuccess ? "步骤完成!" : "再试一次!";
        completionText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        if (isSuccess)
        {
            currentStep++;
            interactionPanel.SetActive(false);
            completionText.gameObject.SetActive(false);

            if (currentStep >= stepButtons.Length)
            {
                GameComplete();
                yield break;
            }
            else
            {
                stepButtons[currentStep].SetActive(true);
                Tip.text = "请开始" + GetStepAction(currentStep) + "…";
            }
        }
        else
        {
            // 完全重置交互状态
            progressSlider.value = 0;
            progressFillImage.color = Color.white;
            completionText.gameObject.SetActive(false);
            stepButtons[currentStep].SetActive(true);
            interactionPanel.SetActive(false);
            Tip.text = "请开始" + GetStepAction(currentStep) + "…";
        }

        isInteracting = false;
        hasPressedSpace = false;
    }

    void UpdateButtonVisibility()
    {
        for (int i = 0; i < stepButtons.Length; i++)
        {
            stepButtons[i].SetActive(i == currentStep);
        }
    }

    void GameComplete()
    {
        isGaming = false;
        finalWinText.SetActive(true);
        isInteracting = false;
        successPanel.SetActive(true);
        Debug.Log("游戏胜利！");
    }

    string GetInstructionText(int step)
    {
        string[] instructions = {
            "长按空格键砍柴",
            "长按空格键洗米洗鱼",
            "长按空格键生火做饭",
            "长按空格键制作菜品"
        };
        return instructions[step];
    }

    string GetStepAction(int step)
    {
        string[] actions = {
            "砍柴",
            "洗米洗鱼",
            "生火做饭",
            "制作菜品"
        };
        return (step >= 0 && step < actions.Length) ? actions[step] : "已完成";
    }

    public void UpdateItemStatus(string itemName)
    {
        if (itemName == "鱼")
        {
            isFishTaken = true;
        }
        else if (itemName == "水稻")
        {
            isRiceTaken = true;
        }
    }
}