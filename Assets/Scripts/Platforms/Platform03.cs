using UnityEngine;
public class Platform03 : MonoBehaviour
{
    [SerializeField] PlatformBase basePlatform;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Funcs.Get().LayerEqualPlayer(other.gameObject.layer))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}