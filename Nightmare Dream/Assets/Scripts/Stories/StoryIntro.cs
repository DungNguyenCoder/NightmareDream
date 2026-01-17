using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class StoryIntro : MonoBehaviour
{
    [TextArea(5, 10)]
    [SerializeField] private string storyText;
    [SerializeField] private float charDelay = 0.2f;
    [SerializeField] private TextMeshProUGUI textUI;

    private void Start()
    {
        StartCoroutine(PlayStory());
    }

    private IEnumerator PlayStory()
    {
        textUI.text = "";

        foreach (char c in storyText)
        {
            textUI.text += c;
            yield return new WaitForSeconds(charDelay);
        }
    }

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("GameScene");
        AudioManager.Instance.PlayGameMusic();
    }
}
