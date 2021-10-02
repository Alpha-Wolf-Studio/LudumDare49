using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
public class Platform02 : MonoBehaviour, IPlatform
{
    private AllSpritesPlatforms allSprites;
    [SerializeField] PlatformBase basePlatform;
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private float minRandom = 1.2f;
    [SerializeField] private float MaxRandom = 3.0f;
    [SerializeField] private float maxTimeAlive;
    private float onTimeAlive;
    private bool firstCollision;
    public SpriteRenderer[] sprites;
    public SpriteRenderer[] glitches;
    private float maxGlitchChanges;
    [SerializeField] private float maxChangeTime = 0.3f;
    private float onChangeTime;

    private void Start()
    {
        maxGlitchChanges = Random.Range(0, allSprites.spritesGlitchs.Length);

        if (!image)
            Debug.LogWarning("Imagen no seteada en el Prefab Platform02.", gameObject);

        if (maxTimeAlive == 0)
        {
            maxTimeAlive = Random.Range(minRandom, MaxRandom);
        }

        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].sprite = allSprites.spritesIcons[Random.Range(0, allSprites.spritesIcons.Length)];
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!firstCollision)
        {
            if (Funcs.Get().LayerEqualPlayer(other.gameObject.layer))
            {
                firstCollision = true;
                StartCoroutine(StartDestroy());
            }
        }
    }
    IEnumerator StartDestroy()
    {
        while (onTimeAlive < maxTimeAlive)
        {
            onTimeAlive += Time.deltaTime;
            onChangeTime += Time.deltaTime;

            if (onChangeTime > maxChangeTime)
            {
                
                onChangeTime = 0;
            }
            
            yield return null;
        }
        basePlatform.DestroyPlatform();
    }

    void IPlatform.DestroyBase()
    {
        basePlatform.DestroyPlatform();
    }
}