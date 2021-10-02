using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action onDie;
    public Action onJump;
    public Action onDoubleJump;
    private BoxCollider2D box;
    private Rigidbody2D rb;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private bool isAlive = true;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private int jumps = 0;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] LayerMask platformLayers;
    [SerializeField] Vector2 initialPosition;
    [SerializeField] float deathY = -20.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        Movement();
        CheckDeath();
    }

    private void Movement()
    {
        if (isGrounded)
        {
            jumps = 0;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.W)) && jumps < maxJumps-1)
        {
            jumps++;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else
        {
            isGrounded = GroundCheck();
        }

        HandleMovement();
    }

    private bool GroundCheck()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector3.down, box.bounds.size.y, platformLayers);
        return raycastHit2D;
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(+speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void CheckDeath()
    {
        if (box.bounds.center.y < deathY)
        {
            isAlive = false;
        }
        if (!isAlive)
        {
            isAlive = true;
            rb.position = initialPosition;
        }
    }
}