using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : Panel
{
    public void OnClickReplay()
    {
        SceneManager.LoadScene("GameScene");
        AudioManager.Instance.PlayMusicFromStart();
    }
    public void OnClickHome()
    {
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.PlayMenuMusic();
    }
}
