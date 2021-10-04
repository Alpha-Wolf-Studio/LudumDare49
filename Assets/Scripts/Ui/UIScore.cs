using UnityEngine;
using System.Collections;
using TMPro;

public class UIScore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI stageText;
    [Header("Fade Configuration")]
    [SerializeField] float timeBeforeFade = 2f;
    [SerializeField] float fadeSpeed = 1f;
    [Header("Glowing Configuration")]
    [SerializeField] float glowingSpeed = 1f;
    [SerializeField] Color glowDownColor;
    [SerializeField] Color glowUpColor;
    bool glowingUp = true;
    float t = 0;
    int currentScore = 0;

    private int currentStage = 0;
    Player player;
    GameManager gameManager;

    // Start is called before the first frame update
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
    }

    // Update is called once per frame
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
        stageText.color = Color.Lerp(glowDownColor, glowUpColor, t);
    }

    void OnScoreChange(int score) 
    {
        currentScore += score;
        scoreText.text = "Score - " + currentScore.ToString();
    }

    void NextStage()
    {
        currentStage++;
        stageText.text = "STAGE - " + currentStage.ToString();
    }

    void ResetStage()
    {
        currentStage=0;
        stageText.text = "STAGE - " + currentStage.ToString();
    }
}
