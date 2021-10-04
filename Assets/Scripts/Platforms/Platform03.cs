using UnityEngine;
using System.Collections;
public class Platform03 : Platform
{
    [Header("Pillars references")] 
    [SerializeField] private GameObject leftSidePlatform;
    [SerializeField] private GameObject rightSidePlatform;
    [SerializeField] private GameObject lowPlatform;

    [Header("Corruption")] 
    [SerializeField] private float timeToRemovePlatform = 1f;
    private float minRandomAlive = 1f;
    private float MaxRandomAlive = 1.5f;
    private float maxTimeAlive;
    private float onTimeAlive;
    public ComponentHasGlitch[] glitches;
    private int currenGlitch = -1;

    private Rigidbody2D rb;
    private Rigidbody2D leftRB;
    private Rigidbody2D rightRB;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        leftRB = leftSidePlatform.GetComponent<Rigidbody2D>();
        rightRB = rightSidePlatform.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.bodyType = RigidbodyType2D.Static;
        leftRB.bodyType = RigidbodyType2D.Static;
        rightRB.bodyType = RigidbodyType2D.Static;
        maxTimeAlive = Random.Range(minRandomAlive, MaxRandomAlive);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Funcs.Get().LayerEqualPlayer(other.gameObject.layer) && !firstCollision)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            leftRB.bodyType = RigidbodyType2D.Dynamic;
            rightRB.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(DestroyLowPlatform());
            other.gameObject.GetComponent<Player>().CollectPoints(scoreOnCorruption);
            for (int i = 0; i < glitches.Length; i++)
            {
                glitches[i].onDoneGlitch += NextGlitch;
            }
            glitches[0].gameObject.SetActive(true);
            NextGlitch();
            StartCoroutine(StartDestroy());
            firstCollision = true;
        }
    }
    IEnumerator DestroyLowPlatform() 
    {
        yield return new WaitForSeconds(timeToRemovePlatform);
        lowPlatform.SetActive(false);
        basePlatform.DestroyPlatform();
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
        if (currenGlitch < glitches.Length)
        {
            glitches[currenGlitch].gameObject.SetActive(true);
            float timePerGlitch = maxTimeAlive / glitches.Length;
            glitches[currenGlitch].SetGlitch(timePerGlitch);
        }
    }
}