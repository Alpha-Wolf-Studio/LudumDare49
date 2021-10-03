using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.Die();
            return;
        }

        Platform platform = collision.GetComponent<Platform>();
        if (platform)
        {
            platform.DestroyBase();
        }
    }
}
