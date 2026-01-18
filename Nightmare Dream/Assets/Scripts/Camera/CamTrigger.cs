using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 _newCamPos;
    [SerializeField] private Transform _newPlayerPos;
    CameraController _cameraController;
    private void Start()
    {
        _cameraController = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == GameConfig.TAG_PLAYER)
        {
            _cameraController._minPos += _newCamPos;
            _cameraController._maxPos += _newCamPos;

            other.transform.position = _newPlayerPos.transform.position;
        }
    }
}