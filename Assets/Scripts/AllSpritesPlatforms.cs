using UnityEngine;
public class AllSpritesPlatforms : MonoBehaviour
{

    static AllSpritesPlatforms instance;
    static public AllSpritesPlatforms Get() => instance;
    private void Awake()
    {
        instance = this;
    }
    public enum TypeSprite
    {
        Searcher,
        Files,
        Glitchs,
        Icons,
        ScrollBar
    }
    public Sprite[] spritesSearcher;
    public Sprite[] spritesFolders;
    public Sprite[] spritesGlitchs;
    public Sprite[] spritesIcons;
    public Sprite[] spritesScrollBar;
}