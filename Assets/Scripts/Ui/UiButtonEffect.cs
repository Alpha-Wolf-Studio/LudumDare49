using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UiButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float scaleMultiply = 3;
    [SerializeField] private float limit = 1.2f;
    [SerializeField] private bool changeColor;
    [SerializeField] private Color mouseHover = Color.green;
    private Color notMouseHover;
    private Image image;
    private bool increment = false;
    private Vector3 initialScale;
    private Vector3 scale;

    private void Awake()
    {
        increment = false;
        initialScale = transform.localScale;
    }

    private void Start()
    {
        if (changeColor)
        {
            image = GetComponent<Image>();
            notMouseHover = image.color;
        }
    }

    private void OnEnable()
    {
        transform.localScale = initialScale;
        increment = false;
    }

    private void Update()
    {
        ChangeScale();
    }

    public void OnMouseEnterButton()
    {
        increment = true;
        if (changeColor)
        {
            image.color = mouseHover;
        }
        // todo: evento de mouse sobre boton ENTRA.
    }
    public void OnMouseExitButton()
    {
        increment = false;
        if (changeColor)
        {
            image.color = notMouseHover;
        }
        // todo: evento de mouse sobre boton SALE.
    }
    private void ChangeScale()
    {
        float timeStep = scaleMultiply * Time.unscaledDeltaTime;
        scale = transform.localScale;
        if (increment)
        {
            if (transform.localScale.x < limit)
            {
                scale = new Vector3(scale.x + timeStep, scale.y + timeStep, scale.z + timeStep);
                transform.localScale = scale;
            }
            else
            {
                transform.localScale = new Vector3(limit, limit, limit);
            }
        }
        else
        {
            if (transform.localScale.x > initialScale.x)
            {
                scale = new Vector3(scale.x - timeStep, scale.y - timeStep, scale.z - timeStep);
                transform.localScale = scale;
            }
            else
            {
                transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnterButton();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExitButton();
    }
}