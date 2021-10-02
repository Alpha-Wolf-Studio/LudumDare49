using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
public class Platform02 : MonoBehaviour, IPlatform
{
    [SerializeField] PlatformBase basePlatform;
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private float minRandom = 1.2f;
    [SerializeField] private float MaxRandom = 3.0f;
    [SerializeField] private float maxTimeAlive;
    private float onTimeAlive;
    private Color colorStart;
    private Color colorEnd = Color.black;
    private bool firstCollision;

    private void Start()
    {
        if (!image)
            Debug.LogWarning("Imagen no seteada en el Prefab Platform02.", gameObject);

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
            image.color = Color.Lerp(colorStart, colorEnd, onTimeAlive / maxTimeAlive);
            yield return null;
        }
        basePlatform.DestroyPlatform();
    }

    void IPlatform.DestroyBase()
    {
        basePlatform.DestroyPlatform();
    }
}