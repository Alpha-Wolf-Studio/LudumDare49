using System;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : MonoBehaviour
{
    public GameManager gm;
    [Serializable] public class Stage
    {
        public Sprite[] wall;
        public Sprite eyes;
        public Sprite eyebrows;
        public Sprite[] fires;
    }
    public List<Stage> stages = new List<Stage>();

    private void Awake()
    {
        if (!gm)
        {
            Debug.LogWarning("GameManager no asignado. ", gameObject);
            gm = FindObjectOfType<GameManager>();
        }
    }
    private void Start()
    {

    }
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
    void FirewallAngry()
    {

    }
    void Stage00()
    {

    }
    void Stage01()
    {

    }
    void Stage02()
    {

    }
    void Stage03()
    {

    }
    void Stage04()
    {

    }
    void Stage05()
    {

    }
}