using UnityEngine.SceneManagement;

public class MainMenuUI : Panel
{
    public void OnClickPlayButton()
    {
        SceneManager.LoadScene(GameConfig.SCENE_STORY);
    }
    public void OnClickModeButton()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_MODE);
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