using UnityEngine;
using System.Collections;
public class Platform03 : Platform
{
    [Header("Pillars references")] 
    [SerializeField] private GameObject leftSidePlatform;
    [SerializeField] private GameObject rightSidePlatform;
    [SerializeField] private GameObject lowPlatform;

    [Space(10)] 
    [SerializeField] private float timeToRemovePlatform = 1f;

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
            firstCollision = true;
        }
    }
    IEnumerator DestroyLowPlatform() 
    {
        yield return new WaitForSeconds(timeToRemovePlatform);
        lowPlatform.SetActive(false);
        basePlatform.DestroyPlatform();
    }
}