using UnityEngine;
using System.Collections;
using TMPro;

public class UIScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI stageText;
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
        stageText.text = "STAGE 0";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = player.points.ToString();
    }

    void ShowChangeState()
    {
        stage++;
        stageText.text = "STAGE " + stage.ToString();
    }

    void ResetStage()
    {
        stage=0;
        stageText.text = "STAGE " + stage.ToString();
    }
}
