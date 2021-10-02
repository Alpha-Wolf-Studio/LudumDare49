using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class Platform02 : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float minRandom = 1.2f;
    [SerializeField] private float MaxRandom = 3.0f;
    [SerializeField] private float maxTimeAlive;
    private float onTimeAlive;
    private Color colorStart;
    private Color colorEnd = Color.black;
    private bool firstCollision;

    private void Start()
    {
        if (maxTimeAlive == 0)
        {
            maxTimeAlive = Random.Range(minRandom, MaxRandom);
        }
        colorStart = image.color;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!firstCollision)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                firstCollision = true;
                StartDestroy();
            }
        }
    }
    IEnumerator StartDestroy()
    {
        while (onTimeAlive < maxTimeAlive)
        {
            onTimeAlive += Time.deltaTime;
            image.color = Color.Lerp(colorStart, colorEnd, onTimeAlive / maxTimeAlive);
            yield return null;
        }
        Destroy(gameObject);
    }
}