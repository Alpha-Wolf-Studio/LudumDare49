using UnityEngine;
using System.Collections;
using TMPro;

public class UIScore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI stageText;
    [Header("Glowing Configuration")]
    [SerializeField] float glowingSpeed = 1f;
    [SerializeField] float alphaSpeed = 0.5f;
    [SerializeField] Color glowDownColor;
    [SerializeField] Color glowUpColor;
    private bool firstShow = true;
    bool glowingUp = true;
    bool alphaUp = true;
    float t = 0;
    int currentScore = 0;
    private Player player;
    private GameManager gameManager;

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.onCollect += OnScoreChange;
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnStageChange += NextStage;
        gameManager.OnResetLevel += ResetStage;
        stageText.text = "STAGE - 0";
        scoreText.text = "Score - 0";
        scoreText.color = glowDownColor;
        stageText.color = glowDownColor;
        stageText.alpha = 0;
    }
    void Update()
    {
        if (glowingUp)
        {
            t += Time.deltaTime * glowingSpeed;
            if (t > 1)
            {
                glowingUp = false;
            }
        }
        else
        {
            t -= Time.deltaTime * glowingSpeed;
            if (t < 0)
            {
                glowingUp = true;
            }
        }
        scoreText.color = Color.Lerp(glowDownColor, glowUpColor, t);
        ShowStage();
    }
    void OnScoreChange(int score) 
    {
        currentScore += score;
        scoreText.text = "Score - " + currentScore.ToString();
    }
    void NextStage(int currentStage)
    {
        stageText.text = "STAGE - " + currentStage.ToString();
        firstShow = true;
        alphaUp = true;
    }
    void ResetStage()
    {
        currentScore = 0;
        scoreText.text = "Score - " + currentScore.ToString();
        stageText.text = "STAGE - 0";
        stageText.color = new Color(stageText.color.r, stageText.color.g, stageText.color.b, 0);
        firstShow = true;
        alphaUp = true;
    }
    void ShowStage()
    {
        if (stageText.color.a < 1 && firstShow && alphaUp)
        {
            stageText.alpha = stageText.color.a + Time.deltaTime * alphaSpeed;
            if (stageText.alpha >= 1)
            {
                alphaUp = false;
            }
        }
        else if (stageText.color.a > 0 && firstShow && !alphaUp)
        {
            stageText.alpha = stageText.color.a - Time.deltaTime * alphaSpeed;
            if (stageText.color.a == 0.0f)
            {
                firstShow = false;
            }
        }
    }
}