using UnityEngine;
public class InGamePause : MonoBehaviour
{
    bool isPause = false;
    public CanvasGroup[] pauseOn;
    public CanvasGroup[] pauseOff;

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

        for (int i = 0; i < pauseOn.Length; i++)
        {
            pauseOn[i].alpha = isPause ? 1 : 0;
            pauseOn[i].blocksRaycasts = isPause;
            pauseOn[i].interactable = isPause;
        }
        for (int i = 0; i < pauseOff.Length; i++)
        {
            pauseOff[i].alpha = !isPause ? 0 : 1;
            pauseOff[i].blocksRaycasts = !isPause;
            pauseOff[i].interactable = !isPause;
        }
    }

    public void BackToMenu()
    {
        SceneManager.Get().LoadSceneAsync("MainMenu");
    }
}