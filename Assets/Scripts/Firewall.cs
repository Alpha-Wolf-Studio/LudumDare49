using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : MonoBehaviour
{
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
            player.Die();
            return;
        }

        PlatformBase platform = collision.collider.GetComponent<PlatformBase>();
        if (platform)
        {
            platform.DestroyPlatform();
        }
    }
}
