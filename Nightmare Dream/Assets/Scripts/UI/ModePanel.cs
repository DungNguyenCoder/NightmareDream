using UnityEngine;
using UnityEngine.UI;

public class ModePanel : Panel
{
    [SerializeField] private Image practiceMode;
    [SerializeField] private Image normalMode;
    [SerializeField] private Image hardMode;
    private Color deselected = Color.white;
    private Color selected = new Color(160f / 255f, 160f / 255f, 160f / 255f);

    private void OnEnable()
    {
        int highlight = PlayerPrefs.GetInt(GameConfig.GAMEMODE_KEY);
        if (highlight == 1) OnClickPracticeButton();
        else if (highlight == 2) OnClickNormalButton();
        else if (highlight == 3) OnClickHardButton();
    }
    public void OnClickPracticeButton()
    {
        EventManager.onUpdateNumberOfNightmare?.Invoke(0);
        practiceMode.color = selected;
        normalMode.color = deselected;
        hardMode.color = deselected;
        PlayerPrefs.SetInt(GameConfig.GAMEMODE_KEY, 1);
        PlayerPrefs.Save();
    }
    public void OnClickNormalButton()
    {
        EventManager.onUpdateNumberOfNightmare?.Invoke(1);
        practiceMode.color = deselected;
        normalMode.color = selected;
        hardMode.color = deselected;
        PlayerPrefs.SetInt(GameConfig.GAMEMODE_KEY, 2);
        PlayerPrefs.Save();
    }
    public void OnClickHardButton()
    {
        EventManager.onUpdateNumberOfNightmare?.Invoke(3);
        practiceMode.color = deselected;
        normalMode.color = deselected;
        hardMode.color = selected;
        PlayerPrefs.SetInt(GameConfig.GAMEMODE_KEY, 3);
        PlayerPrefs.Save();
    }
    public void OnClickBack()
    {
        PanelManager.Instance.ClosePanel(this.name);
    }
}
