using UnityEngine;

public class PathFollowerNightmare : MonoBehaviour
{
    public float delay { get; set; }
    public bool isActive { get; set; }
    public int index = 0;
    private float firstMoveSpeed = 5f;
    public bool isFirstMoveDone { get; set; }

    private void Start()
    {
        isFirstMoveDone = false;
        isActive = true;
        index = 0;
    }

    private void Update()
    {
        if (!isActive) return;

        var snapshots = PlayerRecorder.Instance.snapshots;

        if (snapshots.Count == 0) return;

        float targetTime = Time.time - delay;

        if (snapshots[0].time > targetTime)
            return;

        if (!isFirstMoveDone)
        {
            transform.position = Vector3.MoveTowards(transform.position, snapshots[0].position, firstMoveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, snapshots[0].position) < 0.05f)
            {
                transform.position = snapshots[0].position;
                isFirstMoveDone = true;
                index = 1;
            }
        }
        else
        {
            if (index < snapshots.Count && snapshots[index].time < targetTime)
            {
                transform.position = snapshots[index].position;
                transform.localScale = snapshots[index].localScale;
                index++;
            }
        }
    }
}
