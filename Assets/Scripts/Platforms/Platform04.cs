using UnityEngine;
public class Platform04 : MonoBehaviour, IPlatform
{
    enum Direction
    {
        None,
        Left,
        Right
    }
    private Direction dir = Direction.None;
    private Direction lastDir = Direction.None;

    [SerializeField] PlatformBase basePlatform;
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private SpriteRenderer imageBar;
    private Rigidbody2D rb;

    [SerializeField] private float strengthBarReduction = 20f;
    [SerializeField] private float distance = 0.8f;
    private bool firstCollision;
    private float actualx;
    private float playerx;
    private float barx;
    public GameObject[] holdDirections;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.bodyType = RigidbodyType2D.Static;
        dir = Direction.None;
        barx = transform.position.x;
        if (!image)
            Debug.LogWarning("Imagen no seteada en el Prefab Platform04.", gameObject);
        if (!imageBar)
            Debug.LogWarning("ImageBar no seteada en el Prefab Platform04.", gameObject);
    }
    private void Update()
    {
        if (dir != lastDir)
        {
            lastDir = dir;
            switch (dir)
            {
                case Direction.None:
                    holdDirections[0].SetActive(false);
                    holdDirections[1].SetActive(false);
                    break;
                case Direction.Left:
                    holdDirections[0].SetActive(true);
                    holdDirections[1].SetActive(false);
                    break;
                case Direction.Right:
                    holdDirections[0].SetActive(false);
                    holdDirections[1].SetActive(true);
                    break;
                default:
                    Debug.Log("dir excede el DIrection.");
                    break;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!firstCollision)
        {
            if (Funcs.Get().LayerEqualPlayer(other.gameObject.layer))
            {
                firstCollision = true;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (firstCollision)
        {
            if (Funcs.Get().LayerEqualPlayer(other.gameObject.layer))
            {
                playerx = other.transform.position.x;
                if (playerx < barx)
                {
                    actualx -= strengthBarReduction * Time.deltaTime;
                    dir = Direction.Left;
                }
                else
                {
                    actualx += strengthBarReduction * Time.deltaTime;
                    dir = Direction.Right;
                }

                if (Mathf.Abs(actualx) > distance)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
                else
                {
                    Vector3 posBar = imageBar.transform.localPosition;
                    Debug.Log("Actualizando pos: " + actualx);
                    imageBar.transform.localPosition = new Vector3(actualx, posBar.y, posBar.z);
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (Funcs.Get().LayerEqualPlayer(other.gameObject.layer))
            dir = Direction.None;
    }
    void IPlatform.DestroyBase()
    {
        basePlatform.DestroyPlatform();
    }
}