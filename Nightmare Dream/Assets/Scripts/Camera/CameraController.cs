using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PathFollowerNightmare _nightmare;
    [SerializeField] private float _smoothSpeed;
    private Vector3 _targetPos, _newPos;
    public Vector3 _minPos, _maxPos;

    private Vector3 _deadRoomPos = new Vector3(60, 0, -10);
    private Vector3 _winRoomPos = new Vector3(60, 12, -10);
    private void LateUpdate()
    {
        if (!_player.isDead)
        {
            _targetPos = _player.transform.position;

            Vector3 camBoundaryPos = new Vector3(
                Mathf.Clamp(_targetPos.x, _minPos.x, _maxPos.x),
                Mathf.Clamp(_targetPos.y, _minPos.y, _maxPos.y),
                Mathf.Clamp(_targetPos.z, _minPos.z, _maxPos.z));

            _newPos = Vector3.Lerp(transform.position, camBoundaryPos, _smoothSpeed);
            transform.position = _newPos;
            if (transform.position == _winRoomPos || transform.position == _deadRoomPos)
            {
                _nightmare.isActive = false;
                PanelManager.Instance.OpenPanel(GameConfig.PANEL_WIN);
                AudioManager.Instance.PlayWinMusic();
            }
        }
    }
}
