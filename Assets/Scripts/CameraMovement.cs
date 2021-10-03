using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
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
        gameManager.OnResetLevel += ResetPosition;
    }
    private void Update()
    {
        Movement();
    } 
    private void Movement()
    {
        float playerX = player.transform.position.x;
        var transf = transform;
        Vector3 pos = transf.position;

        if (Mathf.Abs(playerX - transform.position.x) > catchUpDistanceX)
        {
            pos = new Vector3(pos.x + speed * catchUpSpeedMultiplier * Time.deltaTime, pos.y, pos.z);
            transf.position = pos;
        }
        else 
        {
            pos = new Vector3(pos.x + speed * Time.deltaTime, pos.y, pos.z);
            transf.position = pos;
        }
        speed += incrementalSpeed * Time.deltaTime;
    }
    private void ResetPosition()
    {
        transform.position = startingPos;
        incrementalSpeed = startingIncrementalSpeed;
        speed = startingSpeed;
    }
}