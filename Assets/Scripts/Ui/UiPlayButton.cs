using UnityEngine;
public class UiPlayButton : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClipPlay;
    public void PlayButton()
    {
        audioSource.clip = audioClipPlay;
        audioSource.Play();
    }
}