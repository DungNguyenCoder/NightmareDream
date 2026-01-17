using UnityEngine;

public class PathFollowerNightmare : MonoBehaviour
{
    private float delay = 3f;
    public bool isActive = false;
    public int index = 0;

    private void Update()
    {
        if (!isActive) return;     
        var snapshots = PlayerRecorder.Instance.snapshots;

        if (snapshots.Count == 0) return;

        float targetTime = Time.time - delay;

        while (index < snapshots.Count && snapshots[index].time < targetTime)
        {
            transform.position = snapshots[index].position;
            transform.rotation = snapshots[index].rotation;
            transform.localScale = snapshots[index].localScale;
            index++;
        }
    }
}
