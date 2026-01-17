using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "NewStoryLine", menuName = "Story")]
public class Story : ScriptableObject
{
    public string[] storyLines;
    public float typingSpeed = 0.05f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    public bool[] autoProcessLines;
    public float autoProcessDelay = 1.5f;
}
