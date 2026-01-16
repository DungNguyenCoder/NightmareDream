using UnityEngine;

public class PathFollowerNightmare : MonoBehaviour
{
    public float delay = 3f;
    private int index = 0;

    void Update()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("PLAYER DEAD");
            // gọi death logic ở đây
        }
    }
}
