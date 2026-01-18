using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SpawnNightmare _nightmare;
    [SerializeField] private float _smoothSpeed;
    private Vector3 _targetPos, _newPos;
    public Vector3 _minPos { get; set; }
    public Vector3 _maxPos { get; set; }
    private bool hasWon = false;
    private void Awake()
    {
        _minPos = new Vector3(0, 0, -10);
        _maxPos = new Vector3(0, 0, -10);
        transform.position = new Vector3(0, 0, -10);
    }
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
            if (Vector3.Distance(transform.position, GameConfig.POSITION_WIN_ROOM) < 0.01f || Vector3.Distance(transform.position, GameConfig.POSITION_DEAD_ROOM) < 0.01f)
            {
                _nightmare.IsActiveXNightmare(false);
            }
            if(!hasWon && Vector3.Distance(transform.position, GameConfig.POSITION_WIN_ROOM) < 0.01f)
            {
                hasWon = true;
                PanelManager.Instance.OpenPanel(GameConfig.PANEL_WIN);
                AudioManager.Instance.PlayWinMusic();
            }
        }
    }
}
