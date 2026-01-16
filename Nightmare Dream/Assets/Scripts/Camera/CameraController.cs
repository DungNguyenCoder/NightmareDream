using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _smoothSpeed;

    private Vector3 _targetPos, _newPos;
    public Vector3 _minPos, _maxPos;

    private void LateUpdate()
    {
        if(transform.position != _player.position)
        {
            _targetPos = _player.position;

            Vector3 camBoundaryPos = new Vector3(
                Mathf.Clamp(_targetPos.x, _minPos.x, _maxPos.x),
                Mathf.Clamp(_targetPos.y, _minPos.y, _maxPos.y),
                Mathf.Clamp(_targetPos.z, _minPos.z, _maxPos.z));

            _newPos = Vector3.Lerp(transform.position, camBoundaryPos, _smoothSpeed);
            transform.position = _newPos;
        }
    }
}
