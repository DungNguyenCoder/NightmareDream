using UnityEngine;

public class SettingPanel : Panel
{
    public void OnClickCreditButton()
    {
        Application.OpenURL(GameConfig.URL);
    }
    public void OnClickBackButton()
    {
        PanelManager.Instance.ClosePanel(this.name);
    }
}
