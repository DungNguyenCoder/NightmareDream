using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : Panel
{
    public void OnClickReplay()
    {
        SceneManager.LoadScene(GameConfig.SCENE_PLAY);
        AudioManager.Instance.PlayGameMusic();
    }
    public void OnClickHome()
    {
        SceneManager.LoadScene(GameConfig.SCENE_MAIN_MENU);
        AudioManager.Instance.PlayMenuMusic();
    }
}
