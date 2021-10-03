using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action onDie;
    public Action onJump;
    public Action onDoubleJump;
    public Action onGround;
    public Action<int> onCollect;

    private BoxCollider2D box;
    private Rigidbody2D rb;
    private float horizontal;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private int points = 0;
    [SerializeField] private bool isAlive = true;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private int jumps = 0;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] LayerMask platformLayers;
    [SerializeField] Vector2 initialPosition;
    [SerializeField] float deathY = -20.0f;

    [Header("Animation")]
    [SerializeField] string runAnimationBool = "run";
    [SerializeField] string idleAnimationBool = "idle";
    [SerializeField] string isGroundedAnimationBool = "isGrounded";
    [SerializeField] string dieAnimationTrigger = "die";
    SpriteRenderer spriteRenderer;
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);
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

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && jumps < maxJumps-1)
        {
            jumps++;
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            rb.AddForce((Vector2.up * jumpForce), ForceMode2D.Impulse);
        }
        else
        {
            if (GroundCheck())
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        anim.SetBool(isGroundedAnimationBool, isGrounded);
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
            horizontal = -1;
            anim.SetBool(runAnimationBool, true);
            anim.SetBool(idleAnimationBool, false);
            spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            horizontal = 1;
            anim.SetBool(runAnimationBool, true);
            anim.SetBool(idleAnimationBool, false);
            spriteRenderer.flipX = false;
        }
        else
        {
            horizontal = 0;
            anim.SetBool(runAnimationBool, false);
            anim.SetBool(idleAnimationBool, true);
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
            Die();
        }
    }

    public void Die()
    {
        onDie?.Invoke();
        isAlive = true;
        transform.position = initialPosition;
        anim.SetTrigger(dieAnimationTrigger);
    }

    public void CollectPoints(int points)
    {
        this.points += points;
        onCollect?.Invoke(this.points);
    }
}