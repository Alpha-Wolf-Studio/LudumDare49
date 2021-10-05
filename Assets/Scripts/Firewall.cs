using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Firewall : MonoBehaviour
{
    public GameManager gm;
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer spriteRendererFire;
    private int currentStage;
    [Serializable] public class Stage
    {
        public Sprite[] wall;
        public Sprite eyes;
        public Sprite eyebrows;
        public Sprite[] fires;
    }
    public List<Stage> stages = new List<Stage>();

    public SpriteRenderer eyeBrows;
    private bool enableEyeBrows = true;
    public Transform player;
    private int currenWall;
    private int currenFire;
    private float distanceToPlayer = 25;

    private float swapTime = 0.2f;
    private float onSwapTime;
    private float onFireTime;

    private bool isSwapFires;

    private Vector3 initialPos;
    private Vector3 fixPosStage3 = new Vector3(0.357f, 0.568f, 0.0f);
    private Vector3 fixPosStage4 = new Vector3(0.187f, 0.872f, 0.0f);
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!gm)
        {
            Debug.LogWarning("GameManager no asignado. ", gameObject);
            gm = FindObjectOfType<GameManager>();
        }
    }
    private void Start()
    {
        initialPos = eyeBrows.transform.localPosition;
        gm.OnStageChange += LoadStage;
        gm.OnResetLevel += ResetStage;
        ChangeStage();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.Die();
            return;
        }
        Platform platform = collision.GetComponent<Platform>();
        if (platform)
        {
            platform.DestroyBase();
        }
    }
    private void Update()
    {
        //Debug.Log("Pos: " + (player.position.x - transform.position.x).ToString());
        if (player.position.x - transform.position.x < distanceToPlayer)
        {
            if (!enableEyeBrows)
            {
                eyeBrows.gameObject.SetActive(true);
            }
            enableEyeBrows = true;
        }
        else
        {
            if (enableEyeBrows)
            {
                eyeBrows.gameObject.SetActive(false);
            }
            enableEyeBrows = false;
        }

        if (isSwapFires)
        {
            onFireTime += Time.deltaTime;
            if (swapTime < onFireTime)
            {
                onFireTime = 0;
                currenFire++;
                Debug.Log("Current Fire: " + currenFire + "    MaxFires: " + stages[currentStage].fires.Length);
                if (currenFire >= stages[currentStage].fires.Length)
                {
                    currenFire = 0;
                }
                spriteRendererFire.sprite = stages[currentStage].fires[currenFire];
            }
        }
    }

    void ResetStage()
    {
        LoadStage(0);
    }
    void LoadStage(int currentStage)
    {
        if (currentStage < stages.Count)
            this.currentStage = currentStage;
        ChangeStage();
    }
    void ChangeStage()
    {
        StopAllCoroutines();
        spriteRenderer.sprite = stages[currentStage].wall[0];
        eyeBrows.sprite = stages[currentStage].eyebrows;

        if (stages[currentStage].fires.Length == 0) 
            spriteRendererFire.gameObject.SetActive(false);

        if (currentStage == 3)
            eyeBrows.transform.localPosition = fixPosStage3;
        else if (currentStage == 4)
            eyeBrows.transform.localPosition = fixPosStage4;
        else
            eyeBrows.transform.localPosition = initialPos;


        if (stages[currentStage].wall.Length > 1)
        {
            StartCoroutine(nameof(SwapWalls));
        }

        if (stages[currentStage].fires.Length > 1)
        {
            isSwapFires = true;
        }
        else
        {
            isSwapFires = false;
        }
    }
    IEnumerator SwapWalls()
    {
        while (true)
        {
            onSwapTime += Time.deltaTime;
            if (swapTime < onSwapTime)
            {
                onSwapTime = 0;
                currenWall++;
                if (currenWall >= stages[currentStage].wall.Length)
                {
                    currenWall = 0;
                }
                spriteRenderer.sprite = stages[currentStage].wall[currenWall];
            }
            yield return null;
        }
    }
}