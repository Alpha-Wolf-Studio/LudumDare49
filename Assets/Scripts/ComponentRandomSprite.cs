using UnityEngine;
public class ComponentRandomSprite : MonoBehaviour
{
    public AllSpritesPlatforms.TypeSprite typeSprite;
    private AllSpritesPlatforms allSprites;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        allSprites = AllSpritesPlatforms.Get();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        int random = 0;
        switch (typeSprite)
        {
            case AllSpritesPlatforms.TypeSprite.Files:
                random = Random.Range(0, allSprites.spritesFolders.Length);
                spriteRenderer.sprite = allSprites.spritesFolders[random];
                break;
            case AllSpritesPlatforms.TypeSprite.Glitchs:
                random = Random.Range(0, allSprites.spritesGlitchs.Length);
                spriteRenderer.sprite = allSprites.spritesGlitchs[random];
                break;
            case AllSpritesPlatforms.TypeSprite.Icons:
                random = Random.Range(0, allSprites.spritesIcons.Length);
                spriteRenderer.sprite = allSprites.spritesIcons[random];
                break;
            case AllSpritesPlatforms.TypeSprite.ScrollBar:
            case AllSpritesPlatforms.TypeSprite.Searcher:
                Debug.Log("No tiene random este tipo. ", gameObject);
                break;
            default:
                Debug.Log("Default no tiene random este tipo. ", gameObject);
                break;
        }

        // Owinmowe setear Color acá. xd

    }
}