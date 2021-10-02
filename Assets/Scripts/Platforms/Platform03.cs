using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform03 : MonoBehaviour
{

    [SerializeField] PlatformBase basePlatform;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>())
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    
}
