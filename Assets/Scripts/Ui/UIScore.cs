using UnityEngine;
using System.Collections;
using TMPro;

public class UIScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI stageText;
    [Header("Glowing Configuration")]
    [SerializeField] float glowingSpeed = 1f;
    [SerializeField] Color glowDownColor;
    [SerializeField] Color glowUpColor;
    bool glowingUp = true;
    float t = 0;

    private int stage = 0;
    Player player;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnStageChange += ShowChangeState;
        gameManager.OnResetLevel += ResetStage;
        stageText.text = "STAGE - 0";
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
        scoreText.text = "Score - " + player.points.ToString();
    }

    void ShowChangeState()
    {
        stage++;
        stageText.text = "STAGE - " + stage.ToString();
    }

    void ResetStage()
    {
        stage=0;
        stageText.text = "STAGE - " + stage.ToString();
    }
}
