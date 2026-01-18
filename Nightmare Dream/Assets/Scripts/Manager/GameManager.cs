using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int _deadCount { get; set; }
    public int _numberOfNightmare { get; set; }

    void Start()
    {
        if(!PlayerPrefs.HasKey(GameConfig.GAMEMODE_KEY))
        {
            PlayerPrefs.SetInt(GameConfig.GAMEMODE_KEY, 2);
            EventManager.onUpdateNumberOfNightmare?.Invoke(1);
        }
    }
    private void OnEnable()
    {
        EventManager.onUpdateDeadCount += DeadCount;
        EventManager.onUpdateNumberOfNightmare += NightmareCount;
    }
    private void OnDisable()
    {
        EventManager.onUpdateDeadCount += DeadCount;
        EventManager.onUpdateNumberOfNightmare -= NightmareCount;
    }

    private void DeadCount()
    {
        _deadCount++;
    }
    private void NightmareCount(int nightmareCount)
    {
        _numberOfNightmare = nightmareCount;
    }
}