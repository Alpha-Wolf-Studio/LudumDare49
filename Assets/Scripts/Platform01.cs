using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Platform01 : MonoBehaviour
{
    
    private bool firstCollision;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.isKinematic = true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!firstCollision)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                firstCollision = true;
                rb.isKinematic = false;
            }
        }
    }
}
