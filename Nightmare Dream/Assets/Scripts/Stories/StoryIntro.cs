using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class StoryIntro : MonoBehaviour
{
    [TextArea(5, 10)]
    [SerializeField] private string storyText;
    [SerializeField] private float charDelay = 0.4f;
    [SerializeField] private TextMeshProUGUI textUI;

    private void Start()
    {
        StartCoroutine(PlayStory());
    }

    private IEnumerator PlayStory()
    {
        textUI.text = "";
        int i = 0;
        foreach (char c in storyText)
        {
            textUI.text += c;
            i++;
            if (c == '\n')  yield return new WaitForSeconds(1f);
            if (c != '\n' && i % 2 == 1)  AudioManager.Instance.PlaySFX(AudioManager.Instance.voice);
            yield return new WaitForSeconds(charDelay);
        }
    }

    public void OnClickStartButton()
    {
        SceneManager.LoadScene(GameConfig.SCENE_PLAY);
        AudioManager.Instance.PlayGameMusic();
    }
}
