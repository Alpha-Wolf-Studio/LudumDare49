using UnityEngine;
public class Platform04 : MonoBehaviour, IPlatform
{
    [SerializeField] PlatformBase basePlatform;
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private SpriteRenderer imageBar;
    private Player player;

    [SerializeField] private float strengthBarReduction = 20f;
    private bool firstCollision;
    [SerializeField] private float distance = 2.8f;
    private float actualx;
    private float playerx;
    private float barx;

    private void Start()
    {
        barx = transform.position.x;
        if (!image)
            Debug.LogWarning("Imagen no seteada en el Prefab Platform04.", gameObject);
        if (!imageBar)
            Debug.LogWarning("ImageBar no seteada en el Prefab Platform04.", gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!firstCollision)
        {
            if (Funcs.Get().LayerEqualPlayer(other.gameObject.layer))
            {
                player = other.gameObject.GetComponent<Player>();
                firstCollision = true;
            }
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if (firstCollision)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerx = other.transform.position.x;
                if (playerx < barx)
                {
                    actualx -= strengthBarReduction * Time.deltaTime;
                }
                else
                {
                    actualx += strengthBarReduction * Time.deltaTime;
                }

                if (Mathf.Abs(actualx) > distance)
                {
                    basePlatform.DestroyPlatform();
                }
                else
                {
                    Vector3 posBar = imageBar.transform.localPosition;
                    imageBar.transform.localPosition = new Vector3(actualx, posBar.y, posBar.z);
                }
            }
        }
    }
    void IPlatform.DestroyBase()
    {
        basePlatform.DestroyPlatform();
    }
}