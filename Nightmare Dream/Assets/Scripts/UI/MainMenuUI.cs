using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnClickPlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnClickInstructionButton()
    {
        PanelManager.Instance.OpenPanel("InstuctionPanel");
    }
    public void OnClickSettingButton()
    {
        PanelManager.Instance.OpenPanel("Setting");
    }
}