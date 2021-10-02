using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ComponentHasGlitch : MonoBehaviour
{
    private AllSpritesPlatforms allSprites;
    private float onTime;
    [SerializeField] private float maxTime = 2;
    private float nextChangeTime;
    private int maxChanges;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetNewSprite();
    }

    void Update()
    {
        onTime += Time.deltaTime;
        if (onTime > nextChangeTime)
        {
            SetNewSprite();
        }
    }
    void SetNewSprite()
    {
        onTime = 0;
        nextChangeTime = Random.Range(0, maxTime);
        spriteRenderer.sprite = allSprites.spritesGlitchs[Random.Range(0, allSprites.spritesGlitchs.Length)];
    }
}