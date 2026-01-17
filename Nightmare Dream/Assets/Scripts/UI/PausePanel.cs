using UnityEngine;
using UnityEngine.InputSystem;

public class PausePanel : Panel
{
    public void OnClickContinue()
    {
        PanelManager.Instance.ClosePanel(this.name);
        Time.timeScale = 1.0f;
    }
}
