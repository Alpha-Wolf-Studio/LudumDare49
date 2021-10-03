using System.Collections;
using UnityEngine;
public class Platform02 : MonoBehaviour, IPlatform
{
    private AllSpritesPlatforms allSprites;
    [SerializeField] PlatformBase basePlatform;
    private float minRandomAlive = 1.2f;
    private float MaxRandomAlive = 3.0f;
    private float maxTimeAlive;
    private float onTimeAlive;
    private bool firstCollision;
    public ComponentHasGlitch[] glitches;
    private int currenGlitch = -1;

    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        allSprites = AllSpritesPlatforms.Get();
    }
    private void Start()
    {
        maxTimeAlive = Random.Range(minRandomAlive, MaxRandomAlive);
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
                    glitches[i].onDoneGlitch += NextGlitch;
                }

                glitches[0].gameObject.SetActive(true);
                NextGlitch();
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
        rb.bodyType = RigidbodyType2D.Dynamic;
        basePlatform.DestroyPlatform();
    }
    void NextGlitch()
    {
        //Debug.Log("Next Glitch.");
        currenGlitch++;
        if(currenGlitch < glitches.Length) 
        {
            glitches[currenGlitch].gameObject.SetActive(true);
            float timePerGlitch = maxTimeAlive / glitches.Length;
            glitches[currenGlitch].SetGlitch(timePerGlitch);
        }
    }
    void IPlatform.DestroyBase()
    {
        basePlatform.DestroyPlatform();
    }
}