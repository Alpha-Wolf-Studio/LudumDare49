using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationPackage : MonoBehaviour
{
    [SerializeField] private int points = 20;
    [SerializeField] private bool isActive = false;
    private BoxCollider2D box;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player)
        {
            player.CollectPoints();
            return;
        }
    }

    }
