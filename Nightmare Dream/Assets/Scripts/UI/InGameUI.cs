using UnityEngine;

public class InGameUI : Panel
{
    public void OnClickPause()
    {
        AudioManager.Instance.PauseMusic();
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_PAUSE);
        Time.timeScale = 0f;
    }
}
