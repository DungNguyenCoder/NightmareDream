using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : Panel
{
    public void OnClickContinue()
    {
        PanelManager.Instance.ClosePanel(this.name);
        AudioManager.Instance.ContinueMusic();
        Time.timeScale = 1.0f;
    }
    public void OnClickQuit()
    {
        Time.timeScale = 1.0f;
        AudioManager.Instance.PlayMenuMusic();
        SceneManager.LoadScene(GameConfig.SCENE_MAIN_MENU);
    }
}
