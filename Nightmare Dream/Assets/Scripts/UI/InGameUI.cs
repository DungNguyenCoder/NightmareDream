using UnityEngine;

public class InGameUI : Panel
{
    public void OnClickPause()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_PAUSE);
        Time.timeScale = 0f;
    }
}
