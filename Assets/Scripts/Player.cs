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
    [Header("Player Movement")]
    [SerializeField] private int maxJumps = 2;
    [SerializeField] public int points = 0;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private int jumps = 0;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] LayerMask platformLayers;
    [SerializeField] Vector2 initialPosition;
    [SerializeField] float deathY = -10.0f;
    
    bool isAlive = true;
    public float GetSpeed() => speed;

    [Header("Player UI")]
    [SerializeField] GameObject scoreAddTextGo;

    [Header("Animation")]
    [SerializeField] string runAnimationBool = "run";
    [SerializeField] string idleAnimationBool = "idle";
    [SerializeField] string isGroundedAnimationBool = "isGrounded";
    [SerializeField] string dieAnimationTrigger = "die";
    [SerializeField] string reviveAnimationTrigger = "revive";
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
        AudioGameManager.Get().SetPlayer(this);
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
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && jumps < maxJumps)
        {
            Debug.Log("Saltos: " + jumps);
            isGrounded = false;
            jumps++;
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            rb.AddForce((Vector2.up * jumpForce), ForceMode2D.Impulse);
            if (jumps == 1) 
                onJump?.Invoke();
            else 
                onDoubleJump?.Invoke();
        }
        anim.SetBool(isGroundedAnimationBool, isGrounded);
        HandleMovement();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Funcs.Get().LayerEqualPlatform(other.gameObject.layer))
        {
            if (transform.position.y > other.transform.position.y)
            {
                Debug.Log("Reser Jumps.");
                jumps = 0;
                isGrounded = true;
                onGround?.Invoke();
            }
        }
    }

    //private bool GroundCheck()
    //{
    //    RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector3.down, box.bounds.size.y, platformLayers);
    //    return raycastHit2D;
    //}

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
        if (box.bounds.center.y < deathY && isAlive)
        {
            Die();    
        }
    }

    public void Die()
    {
        isAlive = false;
        onDie?.Invoke();
        anim.SetTrigger(dieAnimationTrigger);
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void RevivePlayer() 
    {
        points = 0;
        isAlive = true;
        transform.position = initialPosition;
        rb.velocity = Vector2.zero;
        anim.SetTrigger(reviveAnimationTrigger);
        anim.updateMode = AnimatorUpdateMode.Normal;
    }

    public void CollectPoints(int points)
    {
        onCollect?.Invoke(points);
        this.points += points;
        GameObject go = Instantiate(scoreAddTextGo, transform.position, Quaternion.identity);
        go.GetComponent<UIScoreAddText>().SetScoreText(points);
    }
}