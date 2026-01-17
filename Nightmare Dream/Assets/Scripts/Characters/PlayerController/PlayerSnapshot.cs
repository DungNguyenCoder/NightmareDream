using UnityEngine;

[System.Serializable]
public struct PlayerSnapshot
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 localScale;
    public float time;
    public PlayerSnapshot(Vector3 pos, Quaternion rot, Vector3 scale, float t)
    {
        position = pos;
        rotation = rot;
        localScale = scale;
        time = t;
    }
}
