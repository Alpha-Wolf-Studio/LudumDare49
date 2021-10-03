using UnityEngine;
public class AudioGameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    public enum Sounds     // Esto se ordena dependiendo del array objectsSounds !!
    {
        OnDie,
        OnJump,
        OnDoubleJump,
        OnGround, 
        OnCollectPoints
    }
    public Sounds soundsOrder;
    [SerializeField] private AudioSource[] objectsSounds;

    private void Start()
    {
        if (!player)
        {
            player = FindObjectOfType<Player>();
            Debug.LogWarning("player no está asignado.", gameObject);
        }

        SubscribeAllEvents();         //Todo: Descomentar cuando haya audios.
    }
    private void SubscribeAllEvents()
    {
        player.onDie += OnDie;
        player.onJump += OnJump;
        player.onDoubleJump += OnDoubleJump;
        player.onGround += OnGround;
        player.onCollect += OnCollectPoints;
    }
    private void OnDie()
    {
        objectsSounds[(int) Sounds.OnDie].Play();
    }
    private void OnJump()
    {
        objectsSounds[(int) Sounds.OnJump].Play();
    }
    private void OnDoubleJump()
    {
        objectsSounds[(int) Sounds.OnDoubleJump].Play();
    }
    private void OnGround()
    {
        objectsSounds[(int) Sounds.OnGround].Play();
    }
    private void OnCollectPoints(int points)
    {
        objectsSounds[(int) Sounds.OnCollectPoints].Play();
    }
}