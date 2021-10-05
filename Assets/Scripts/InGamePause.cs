using UnityEngine;
public class InGamePause : MonoBehaviour
{
    bool isPause = false;
    public CanvasGroup panelPause;
    public CanvasGroup panelGame;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Pause();
        }
    }
    public void Pause()
    {
        Time.timeScale = isPause ? 1 : 0;
        isPause = !isPause;

        if (isPause)
        {
            EnableCanvasGroup(panelGame, false);
            EnableCanvasGroup(panelPause, true);
        }
        else
        {
            EnableCanvasGroup(panelGame, true);
            EnableCanvasGroup(panelPause, false);
        }
    }
    void EnableCanvasGroup(CanvasGroup canvasGroup, bool on)
    {
        canvasGroup.alpha = on ? 1 : 0;
        canvasGroup.blocksRaycasts = on;
        canvasGroup.interactable = on;
    }
    public void ChangeVolumeMaster(float vol)
    {
        AudioGameManager.Get().SetMasterVolume(vol);
    }
    public void ChangeVolumeEffect(float vol)
    {
        AudioGameManager.Get().SetEffectVolume(vol);
    }
    public void ChangeVolumeMusic(float vol)
    {
        AudioGameManager.Get().SetMusicVolume(vol);
    }
    public void BackToMenu()
    {
        SceneManager.Get().LoadSceneAsync("MainMenu");
    }
}