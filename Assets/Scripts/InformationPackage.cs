using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class InformationPackage : MonoBehaviour
{
    private AllSpritesPlatforms allSprites;
    [SerializeField] private int minPoints = 20;
    [SerializeField] private int maxPoints = 100;
    private int currentPoints = 0;
    private BoxCollider2D box;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        currentPoints = Random.Range(minPoints, maxPoints);
        allSprites = AllSpritesPlatforms.Get();
        int randomPosition = Random.Range(0, allSprites.spritesFolders.Length);
        GetComponent<SpriteRenderer>().sprite = allSprites.spritesFolders[randomPosition];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.CollectPoints(currentPoints);
            Destroy(gameObject);
            return;
        }
    }
}