using System.Collections.Generic;
using UnityEngine;

public class SpawnNightmare : MonoBehaviour
{
    [SerializeField] private PathFollowerNightmare pathFollowerNightmarePrefab;
    private List<PathFollowerNightmare> nightmares = new List<PathFollowerNightmare>();

    private void Start()
    {
        SpawnXNightmare();
    }
    public void SpawnXNightmare()
    {
        for (int i = 0; i < GameManager.Instance._numberOfNightmare; i++)
        {
            PathFollowerNightmare nightmare = Instantiate(pathFollowerNightmarePrefab, this.transform);
            nightmares.Add(nightmare);
            nightmare.transform.position = new Vector3(-8, 1 - 0.8f * i, 0);
            nightmare.delay = 2f + 0.5f*i;
        }
    }

    public void IsActiveXNightmare(bool active)
    {
        foreach(var nightmare in nightmares) 
        {
            nightmare.isActive = active;
        }
    }
    public void SetStateNightmare()
    {
        int i = 0;
        foreach(var nightmare in nightmares) 
        {
            nightmare.index = 0;
            nightmare.isFirstMoveDone = false;
            nightmare.transform.position = new Vector3(-8, 1 - 0.8f * i, 0);
            i++;
        }
    }
}