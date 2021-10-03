using System;
using System.Collections;
using UnityEngine;
public class PlatformBase : MonoBehaviour
{
    [SerializeField] float timeToDestroyPlatform = 10f;
    [SerializeField] SpriteRenderer[] spritesToColor;

    public Action<PlatformBase> OnDestroy;
    public void DestroyPlatform()
    {
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine() 
    {
        yield return new WaitForSeconds(timeToDestroyPlatform);
        Destroy(gameObject);
        OnDestroy?.Invoke(this);
    }

    public void SetSpritesColor(Color color) 
    {
        foreach (var sprite in spritesToColor)
        {
            sprite.color = color;
        }
    }

}