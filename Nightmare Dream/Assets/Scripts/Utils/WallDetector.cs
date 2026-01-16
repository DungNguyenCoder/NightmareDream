using System;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    [SerializeField] private Transform _wallCheckPos;
    [SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 0.03f);
    [SerializeField] private LayerMask _wallLayer;
    public bool WallCheck()
    {
        return Physics2D.OverlapBox(_wallCheckPos.position, _wallCheckSize, 0, _wallLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_wallCheckPos.position, _wallCheckSize);
    }
}