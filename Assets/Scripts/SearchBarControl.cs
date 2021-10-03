using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchBarControl : MonoBehaviour
{
    [SerializeField] TextMeshPro textComponent = null;
    [SerializeField] List<string> possibleTexts = null;
    [SerializeField] float timeBetweenLetters = .1f;
    string currentText = "Placeholder Text";
    private void Awake()
    {
        if(possibleTexts.Count > 0) 
        {
            int textIndex = Random.Range(0, possibleTexts.Count);
            currentText = possibleTexts[textIndex];
            StartCoroutine(TextCoroutine());
        }
    }

    IEnumerator TextCoroutine() 
    {
        int currentLetter = 0;
        while(currentLetter < currentText.Length) 
        {
            textComponent.text += currentText[currentLetter];
            yield return new WaitForSeconds(timeBetweenLetters);
            currentLetter++;
        }
    }

}
