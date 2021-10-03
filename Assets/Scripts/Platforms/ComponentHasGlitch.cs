using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class ComponentHasGlitch : MonoBehaviour
{
    public Action onDoneGlitch;

    private AllSpritesPlatforms allSprites;
    private float onTime;
    private float nextChangeTime;
    private int currentChanges;
    private int maxChanges;
    private SpriteRenderer spriteRenderer;
    private float glitchTime;

    private void Awake()
    {
        allSprites = AllSpritesPlatforms.Get();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        maxChanges = Random.Range(3, allSprites.spritesGlitchs.Length);
        nextChangeTime = glitchTime / maxChanges;
        SetNewSprite();
    }
    void Update()
    {
        if (currentChanges <= maxChanges)
        {
            onTime += Time.deltaTime;
            if (onTime > nextChangeTime)
            {
                SetNewSprite();
            }
        }
    }
    public void SetGlitch(float timePerGlitch)
    {
        glitchTime = timePerGlitch;
    }
    void SetNewSprite()
    {
        //Debug.Log("Seteo un nuevo Sprite.");
        onTime = 0;
        int random = Random.Range(0, allSprites.spritesGlitchs.Length);
        spriteRenderer.sprite = allSprites.spritesGlitchs[random];
        if (currentChanges >= maxChanges)
            onDoneGlitch?.Invoke();
        currentChanges++;
    }
}