using System;
using UnityEngine;
public class PlatformBase : MonoBehaviour
{
    public Action<PlatformBase> OnDestroy;
    public void DestroyPlatform()
    {
        OnDestroy?.Invoke(this);
        Destroy(gameObject);
    }
}