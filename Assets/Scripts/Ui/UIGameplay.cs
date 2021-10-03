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
    [SerializeField] Transform cameraTransform = null;
    [SerializeField] float multiplier = 0.02f;
    float aux = 0;
    private void Update()
    {
        if(cameraTransform.position.x > aux + 1300) 
        {
            aux += 1300;
        }
        Rect newRect = new Rect((cameraTransform.position.x - aux) * multiplier, 0, 1, 1);
        backgroundImage.uvRect = newRect;
        backgroundImage.color = gameManager.currentThemeColor;
    }
}
