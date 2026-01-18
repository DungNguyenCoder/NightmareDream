using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : Singleton<AudioManager>
{
    public AudioSource audioSource;
    public AudioSource SFXSource;
    public AudioMixer audioMixer;
    public AudioClip run;
    public AudioClip jump;
    public AudioClip dash;
    public AudioClip dead;
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip winMusic;
    public AudioClip voice;
    public override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey(GameConfig.MUSIC_VOLUME_KEY))
        {
            float vol = PlayerPrefs.GetFloat(GameConfig.MUSIC_VOLUME_KEY);
            audioMixer.SetFloat("Music", Mathf.Log10(vol) * 20);
        }
        if (PlayerPrefs.HasKey(GameConfig.SFX_VOLUME_KEY))
        {
            float vol = PlayerPrefs.GetFloat(GameConfig.SFX_VOLUME_KEY);
            audioMixer.SetFloat("SFX", Mathf.Log10(vol) * 20);
        }
    }
    public void PlaySFX(AudioClip audioClip)
    {
        SFXSource.PlayOneShot(audioClip);
    }
    public void PlayMenuMusic()
    {
        if (audioSource.clip == menuMusic) return;

        audioSource.clip = menuMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayGameMusic()
    {
        if (audioSource.clip == gameMusic) return;

        audioSource.clip = gameMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayWinMusic()
    {
        if (audioSource.clip == winMusic) return;

        audioSource.clip = winMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void PauseMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }
    public void ContinueMusic()
    {
        if (audioSource != null)
        {
            audioSource.UnPause();
        }
    }
    public void PlayMusicFromStart()
    {
        audioSource.Stop();
        audioSource.time = 0f;
        audioSource.Play();
    }
}
