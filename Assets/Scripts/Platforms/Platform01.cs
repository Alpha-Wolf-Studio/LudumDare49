using UnityEngine;
public class Platform01 : MonoBehaviour, IPlatform
{
    private AllSpritesPlatforms allSprites;
    [SerializeField] PlatformBase basePlatform;
    private bool firstCollision;
    Rigidbody2D rb;
    public SpriteRenderer[] sprites;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].sprite = allSprites.spritesIcons[Random.Range(0, allSprites.spritesIcons.Length)];
        }
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
    void IPlatform.DestroyBase() 
    {
        basePlatform.DestroyPlatform();
    }
}