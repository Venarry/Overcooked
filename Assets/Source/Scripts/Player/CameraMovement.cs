using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed = 0.25f;

    private Vector3 _targetPosition => _target.position + _offset;

    private void Start()
    {
        transform.position = _targetPosition;
    }

    private void LateUpdate()
    {
        MoveLerp();
    }

    private void MoveLerp()
    {
        Vector3 targetLerpPosition = Vector3.Lerp(transform.position, _targetPosition, _speed);
        transform.position = targetLerpPosition;
    }
}
