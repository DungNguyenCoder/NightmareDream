using TMPro;
using UnityEngine;

public class InGameUI : Panel
{
    [SerializeField] private TextMeshProUGUI _deadCount;

    private void OnEnable()
    {
        EventManager.onUpdateDeadCount += DeadCountUI;
    }
    private void OnDisable()
    {
        EventManager.onUpdateDeadCount += DeadCountUI;
    }
    private void DeadCountUI()
    {
        _deadCount.text = GameManager.Instance._deadCount.ToString();
    }

    public void OnClickPause()
    {
        AudioManager.Instance.PauseMusic();
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_PAUSE);
        Time.timeScale = 0f;
    }
}
