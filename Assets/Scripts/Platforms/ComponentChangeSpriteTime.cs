using UnityEngine;
public class ComponentChangeSpriteTime : MonoBehaviour
{
    private AllSpritesPlatforms allSprites;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private AllSpritesPlatforms.TypeSprite typeSprite = AllSpritesPlatforms.TypeSprite.Glitchs;
    [SerializeField] private float changeTime;
    [SerializeField] private float maxRandomTime = 2;
    private float onTime;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        allSprites = AllSpritesPlatforms.Get();
        if (changeTime == 0)
        {
            changeTime = Random.Range(0, maxRandomTime);
        }
    }
    void Update()
    {
        onTime += Time.deltaTime;
        if (onTime < changeTime)
        {
            onTime = 0;
            int random = 0;
            switch (typeSprite)
            {
                case AllSpritesPlatforms.TypeSprite.Folders:
                    random = Random.Range(0, allSprites.spritesFolders.Length);
                    spriteRenderer.sprite = allSprites.spritesGlitchs[random];
                    break;
                case AllSpritesPlatforms.TypeSprite.Glitchs:
                    random = Random.Range(0, allSprites.spritesGlitchs.Length);
                    spriteRenderer.sprite = allSprites.spritesGlitchs[random];
                    break;
                case AllSpritesPlatforms.TypeSprite.Icons:
                    random = Random.Range(0, allSprites.spritesIcons.Length);
                    spriteRenderer.sprite = allSprites.spritesGlitchs[random];
                    break;
                default:
                    break;
            }

        }
    }
}