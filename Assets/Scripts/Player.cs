using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action onDie;
    public Action onJump;
    public Action onDoubleJump;
    [SerializeField] private float vida = 5;

    void Start()
    {
        
    }
    void Update()
    {
        if (vida < 0)
            onDie?.Invoke();    // Siempre poner el "?" cuando se llama a un evento.
    }
    private void OnDestroy()
    {

    }
}