using UnityEngine;
public class ComponentSpriteFadeAlpha : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color colorStart;
    public Color colorEnd;
    public float changeTime;
    private float onTime;
    private bool toEnd = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        spriteRenderer.color = colorStart;
    }
    void Update()
    {
        onTime += Time.deltaTime;
        if (onTime > changeTime)
        {
            onTime = 0;
            toEnd = !toEnd;
        }
        spriteRenderer.color = toEnd ? Color.Lerp(colorStart, colorEnd, onTime / changeTime) : Color.Lerp(colorEnd, colorStart, onTime / changeTime);
    }
}
