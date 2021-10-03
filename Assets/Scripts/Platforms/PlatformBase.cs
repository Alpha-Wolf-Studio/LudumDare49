using System;
using UnityEngine;
public class PlatformBase : MonoBehaviour
{

    [SerializeField] SpriteRenderer[] spritesToColor;

    public Action<PlatformBase> OnDestroy;
    public void DestroyPlatform()
    {
        OnDestroy?.Invoke(this);
        Destroy(gameObject);
    }

    public void SetSpritesColor(Color color) 
    {
        foreach (var sprite in spritesToColor)
        {
            sprite.color = color;
        }
    }

}