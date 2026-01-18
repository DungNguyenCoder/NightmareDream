using UnityEngine;

[System.Serializable]
public struct PlayerSnapshot
{
    public Vector3 position;
    public Vector3 localScale;
    public float time;
    public PlayerSnapshot(Vector3 pos, Vector3 scale, float t)
    {
        position = pos;
        localScale = scale;
        time = t;
    }
}
