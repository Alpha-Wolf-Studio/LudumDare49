using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [Space(10)]
    [SerializeField] private float speed;
    [SerializeField] private float incrementalSpeed;
    [SerializeField] private float catchUpDistanceX;
    [SerializeField] private float catchUpSpeedMultiplier;

    private void Awake()
    {
        if (!playerTransform)
        {
            playerTransform = FindObjectOfType<Player>().transform;
            Debug.LogWarning("playerTransform no está asignado", gameObject);
        }
    }
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        float playerX = playerTransform.position.x;
        if (Mathf.Abs(playerX - transform.position.x) > catchUpDistanceX) 
        {
            transform.position = new Vector3(transform.position.x + speed * catchUpSpeedMultiplier * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else 
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        speed += incrementalSpeed * Time.deltaTime;
    }
}