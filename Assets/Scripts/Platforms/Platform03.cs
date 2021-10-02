using UnityEngine;
using System.Collections;
public class Platform03 : MonoBehaviour, IPlatform
{
    [Header("Pillars references")]
    [SerializeField] PlatformBase basePlatform = null;
    [SerializeField] GameObject leftSidePlatform = null;
    [SerializeField] GameObject rightSidePlatform = null;
    [SerializeField] GameObject lowPlatform = null;
    [Space(10)]
    [SerializeField] float timeToRemovePlatform = 1f;

    Rigidbody2D rb;
    Rigidbody2D leftRB;
    Rigidbody2D rightRB;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        leftRB = leftSidePlatform.GetComponent<Rigidbody2D>();
        rightRB = rightSidePlatform.GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Funcs.Get().LayerEqualPlayer(other.gameObject.layer))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            leftRB.bodyType = RigidbodyType2D.Dynamic;
            rightRB.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(DestroyLowPlatform());
        }
    }

    IEnumerator DestroyLowPlatform() 
    {
        yield return new WaitForSeconds(timeToRemovePlatform);
        lowPlatform.SetActive(false);
    }

    void IPlatform.DestroyBase()
    {
        basePlatform.DestroyPlatform();
    }
}