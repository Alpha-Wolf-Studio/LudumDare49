using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;

    void Start()
    {
        player.onDie += PlayerDie;
    }
    void Update()
    {
        
    }
    void PlayerDie()
    {

    }
}