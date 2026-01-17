using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PausePanel : Panel
{
    public void OnClickContinue()
    {
        Debug.Log("Continue");
        PanelManager.Instance.ClosePanel(this.name);
        AudioManager.Instance.ContinueMusic();
        Time.timeScale = 1.0f;
    }
    public void OnClickQuit()
    {
        Debug.Log("Quit");
        Time.timeScale = 1.0f;
        AudioManager.Instance.PlayMenuMusic();
        SceneManager.LoadScene("MainMenu");
    }
}
