using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScoreAddText : MonoBehaviour
{
    [SerializeField] TextMeshPro scoreTextComponent = null;
    [SerializeField] float destroyTime = 2f;
    [SerializeField] float upSpeed = 1f;

    public void SetScoreText(int points) 
    {
        scoreTextComponent.text = "+" + points;
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * upSpeed, transform.position.z);
        if(scoreTextComponent.color.a - Time.deltaTime > 0) 
        {
            scoreTextComponent.color = new Color(scoreTextComponent.color.r, scoreTextComponent.color.g, scoreTextComponent.color.b, scoreTextComponent.color.a - Time.deltaTime);
        }
        else 
        {
            scoreTextComponent.color = new Color(0, 0, 0, 0);
        }
    }
}
