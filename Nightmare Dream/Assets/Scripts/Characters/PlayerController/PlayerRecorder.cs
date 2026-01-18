using System.Collections.Generic;
using UnityEngine;

public class PlayerRecorder : Singleton<PlayerRecorder>
{
    public float recordInterval = 0.02f;
    private float timer;
    public List<PlayerSnapshot> snapshots = new List<PlayerSnapshot>();
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= recordInterval)
        {
            timer = 0f;
            snapshots.Add(new PlayerSnapshot(transform.position, transform.localScale, Time.time));
        }
    }
}
