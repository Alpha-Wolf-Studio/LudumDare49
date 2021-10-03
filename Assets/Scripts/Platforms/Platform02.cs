using System.Collections;
using UnityEngine;
public class Platform02 : MonoBehaviour, IPlatform
{
    private AllSpritesPlatforms allSprites;
    [SerializeField] PlatformBase basePlatform;
    private float minRandomAlive = 1.2f;
    private float MaxRandomAlive = 4.0f;
    [SerializeField] private float maxTimeAlive;
    private float onTimeAlive;
    private bool firstCollision;
    public GameObject[] glitches;

    private void Awake()
    {
        allSprites = AllSpritesPlatforms.Get();
    }
    private void Start()
    {
        if (maxTimeAlive == 0)
        {
            maxTimeAlive = Random.Range(minRandomAlive, MaxRandomAlive);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!firstCollision)
        {
            if (Funcs.Get().LayerEqualPlayer(other.gameObject.layer))
            {
                firstCollision = true;
                for (int i = 0; i < glitches.Length; i++)
                {
                    glitches[i].SetActive(true);
                }
                StartCoroutine(StartDestroy());
            }
        }
    }
    IEnumerator StartDestroy()
    {
        while (onTimeAlive < maxTimeAlive)
        {
            onTimeAlive += Time.deltaTime;
            yield return null;
        }
        basePlatform.DestroyPlatform();
    }
    void IPlatform.DestroyBase()
    {
        basePlatform.DestroyPlatform();
    }
}