using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Player player;
    [Space(10)]
    [SerializeField] private float speed;
    [SerializeField] private float incrementalSpeed;
    [SerializeField] private float catchUpDistanceX;
    [SerializeField] private float catchUpSpeedMultiplier;

    private float startingIncrementalSpeed;
    private float startingSpeed;
    private Vector3 startingPos;
    private void Awake()
    {
        startingPos = transform.position;
        startingIncrementalSpeed = incrementalSpeed;
        startingSpeed = speed;

        if (!player)
        {
            player = FindObjectOfType<Player>();
            Debug.LogWarning("playerTransform no está asignado", gameObject);
        }
        player.onDie += ResetPosition;
    }
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        float playerX = player.transform.position.x;
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

    void ResetPosition()
    {
        transform.position = startingPos;
        incrementalSpeed = startingIncrementalSpeed;
        speed = startingSpeed;
    }
}