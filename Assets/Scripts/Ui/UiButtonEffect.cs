using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class UiButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Image")]
    [SerializeField] private bool changeColor;
    [SerializeField] private float scaleMultiply = 3;
    [SerializeField] private float limit = 1.2f;
    [SerializeField] private Color mouseHover = Color.green;
    private Color notMouseHover;
    private Image image;
    private Vector3 initialScale;
    private Vector3 scale;
    private bool increment;
    
    [Header("Text")]
    [SerializeField] private bool changeColorText;
    [SerializeField] private TextMeshProUGUI textMesh;
    private Color notMouseHoverText;

    [Header("Sound")] 
    private AudioSource audioSource;

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
        if (changeColorText && textMesh)
        {
            notMouseHoverText = textMesh.color;
        }
        audioSource = GetComponent<AudioSource>();
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
        if (changeColorText && textMesh)
        {
            textMesh.color = mouseHover;
        }
        audioSource.Play();
    }
    public void OnMouseExitButton()
    {
        increment = false;
        if (changeColor)
        {
            image.color = notMouseHover;
        }
        if (changeColorText && textMesh)
        {
            textMesh.color = notMouseHoverText;
        }
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