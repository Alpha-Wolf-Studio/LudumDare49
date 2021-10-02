using UnityEngine;
public class Platform01 : MonoBehaviour
{
    [SerializeField] PlatformBase basePlatform;
    private bool firstCollision;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.isKinematic = true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!firstCollision)
        {
            if (Funcs.Get().LayerEqualPlayer(other.gameObject.layer))
            {
                firstCollision = true;
                rb.isKinematic = false;
            }
        }
    }
}