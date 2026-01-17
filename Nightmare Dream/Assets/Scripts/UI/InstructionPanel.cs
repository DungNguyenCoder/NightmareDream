using UnityEngine;

public class InstructionPanel : Panel
{
    public void OnClose()
    {
        PanelManager.Instance.ClosePanel(this.name);
    }
}
