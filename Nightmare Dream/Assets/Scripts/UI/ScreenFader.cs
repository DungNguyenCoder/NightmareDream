using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    public IEnumerator FadeOut(float duration = 1f)
    {
        yield return Fade(0f, 1f, duration);
    }

    public IEnumerator FadeIn(float duration = 1f)
    {
        yield return Fade(1f, 0f, duration);
    }


    private IEnumerator Fade(float from, float to, float duration)
    {
        float t = 0f;
        Color c = fadeImage.color;

        while (t < duration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, t / duration);
            fadeImage.color = c;
            yield return null;
        }

        c.a = to;
        fadeImage.color = c;
    }
}