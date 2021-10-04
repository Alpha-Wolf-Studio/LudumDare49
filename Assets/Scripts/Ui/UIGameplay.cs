using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
    [Header("General References")]
    [SerializeField] GameManager gameManager;
    [Header("Background Configuration")]
    [SerializeField] RawImage backgroundImage = null;
    [SerializeField] float backgroundAlphaColorMultiplier = 0.5f;
    [SerializeField] Transform cameraTransform = null;
    [SerializeField] float multiplier = 0.02f;
    float aux = 0;

    Rect startingRect;

    private void Awake()
    {
        startingRect = backgroundImage.uvRect;
    }

    private void Start()
    {
        AudioGameManager.Get().GameplayMusic();    
    }

    private void Update()
    {
        if(cameraTransform.position.x > aux + 1300) 
        {
            aux += 1300;
        }
        Rect newRect = new Rect((cameraTransform.position.x - aux) * multiplier, startingRect.y, startingRect.width, startingRect.height);
        backgroundImage.uvRect = newRect;
        backgroundImage.color = new Color(gameManager.currentThemeColor.r, gameManager.currentThemeColor.g, gameManager.currentThemeColor.b, gameManager.currentThemeColor.a * backgroundAlphaColorMultiplier);
    }
}
