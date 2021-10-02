using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform03 : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Player>()) 
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
