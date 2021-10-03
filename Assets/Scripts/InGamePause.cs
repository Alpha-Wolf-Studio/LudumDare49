using UnityEngine;
public class InGamePause : MonoBehaviour
{
    bool isPause = false;
    public CanvasGroup[] ui;

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

        for (int i = 0; i < ui.Length; i++)
        {
            ui[i].alpha = isPause ? 1 : 0;
            ui[i].blocksRaycasts = isPause;
            ui[i].interactable = isPause;
        }
    }
}