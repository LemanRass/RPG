using UnityEngine;

public class CameraView : MonoBehaviour
{
    public Camera camera => _camera;

    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;
    [Space]
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _clampAngle;
    
    private float _x;
    private float _y;
    
    private void Start()
    {
        _x = transform.rotation.eulerAngles.y;
        _y = transform.rotation.eulerAngles.x;
    }

    private void LateUpdate()
    {
        _distance -= Input.GetAxis("Mouse ScrollWheel") * 2;
        _distance = Mathf.Clamp(_distance, _minDistance, _maxDistance);
        
        if (Input.GetMouseButton(0))
        {
            _x += Input.GetAxis("Mouse X") * _speed * Time.deltaTime;
            _y -= Input.GetAxis("Mouse Y") * _speed * Time.deltaTime;

            _y = Mathf.Clamp(_y, 0.0f, _clampAngle);
        }
        
        var rotation = Quaternion.Euler(_y, _x, 0.0f);
        var position = rotation * new Vector3(0.0f, 0.0f, -_distance) + _player.position + _offset;

        transform.rotation = rotation;
        transform.position = position;
    }
}