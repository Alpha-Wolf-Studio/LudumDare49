using UnityEngine;
public class Platform01 : MonoBehaviour, IPlatform
{
    private AllSpritesPlatforms allSprites;
    [SerializeField] private PlatformBase basePlatform;
    [SerializeField] private GameObject searchBarGO;
    private bool firstCollision;
    private Rigidbody2D rb;
    public SpriteRenderer[] sprites;
    private void Awake()
    {
        allSprites = AllSpritesPlatforms.Get();
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
                if(searchBarGO) searchBarGO.SetActive(true);
                basePlatform.DestroyPlatform();
            }
        }
    }
    void IPlatform.DestroyBase() 
    {
        basePlatform.DestroyPlatform();
    }
}