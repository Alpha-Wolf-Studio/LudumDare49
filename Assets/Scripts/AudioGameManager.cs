using UnityEngine;
using UnityEngine.Audio;
public class AudioGameManager : MonoBehaviourSingleton<AudioGameManager>
{
    [SerializeField] private Player player;
    [SerializeField] private AudioClip[] musics;
    [SerializeField] private AudioSource musicSource;
    public AudioMixer[] audioMixer;
    private float maxVol = 20;
    private float minVol = -30;
    private float distVol;
    

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
        if (player)SubscribeAllEvents();         //Todo: Descomentar cuando haya audios.
    }

    public void SetPlayer(Player p)
    {
        player = p;
        SubscribeAllEvents();
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

    public void SetMasterVolume(float volume)
    {
        distVol = Mathf.Abs(maxVol - minVol) / 10;
        if (volume < 1)
        {
            audioMixer[0].SetFloat("MasterVolume", -80);
        }
        else
        {
            audioMixer[0].SetFloat("MasterVolume", minVol + volume * distVol);
        }
    }

    public void SetEffectVolume(float volume)
    {
        distVol = Mathf.Abs(maxVol - minVol) / 10;
        if (volume < 1)
        {
            audioMixer[1].SetFloat("EffectsVolume", -80);
        }
        else
        {
            audioMixer[1].SetFloat("EffectsVolume", minVol + volume * distVol);
        }

    }
    public void SetMusicVolume(float volume)
    {
        distVol = Mathf.Abs(maxVol - minVol) / 10;
        if (volume < 1)
        {
            audioMixer[2].SetFloat("MusicVolume", -80);
        }
        else
        {
            audioMixer[2].SetFloat("MusicVolume", minVol + volume * distVol);
        }
    }

    public void MenuMusic()
    {
        musicSource.clip = musics[0];
        musicSource.Play();
    }

    public void GameplayMusic()
    {
        musicSource.clip = musics[1];
        musicSource.Play();
    }
}