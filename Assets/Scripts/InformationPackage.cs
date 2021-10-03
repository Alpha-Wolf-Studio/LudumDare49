using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class InformationPackage : MonoBehaviour
{
    [SerializeField] private int minPoints = 20;
    [SerializeField] private int maxPoints = 100;
    int currentPoints = 0;
    private BoxCollider2D box;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        currentPoints = Random.Range(minPoints, maxPoints);
        var allSprites = AllSpritesPlatforms.Get();
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
