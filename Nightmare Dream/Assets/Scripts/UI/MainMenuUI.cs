using UnityEngine.SceneManagement;

public class MainMenuUI : Panel
{
    public void OnClickPlayButton()
    {
        SceneManager.LoadScene("StoryScene");
    }
    public void OnClickInstructionButton()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_INSTRUCTION);
    }
    public void OnClickSettingButton()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_SETTING);
    }
}