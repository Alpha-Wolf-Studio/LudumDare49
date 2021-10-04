using UnityEngine;
public class Platform01 : Platform
{
    private AllSpritesPlatforms allSprites;
    [SerializeField] private GameObject searchBarGO;
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
                other.gameObject.GetComponent<Player>().CollectPoints(scoreOnCorruption);
            }
        }
    }
}