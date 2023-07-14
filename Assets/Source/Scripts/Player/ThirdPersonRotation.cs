using UnityEngine;

public class ThirdPersonRotation : MonoBehaviour
{
    private const string VectorHorizontal = "Horizontal";
    private const string VectorVertical = "Vertical";

    [SerializeField] private float _speed = 0.2f;
    private Vector3 _moveDirection;

    private void FixedUpdate()
    {
        _moveDirection.x = Input.GetAxisRaw(VectorHorizontal);
        _moveDirection.z = Input.GetAxisRaw(VectorVertical);
        _moveDirection.y = 0;

        _moveDirection = _moveDirection.normalized;

        if (!(_moveDirection.magnitude > 0)) 
            return;
        
        Quaternion rotateDirection = Quaternion.LookRotation(_moveDirection);
        Quaternion targetRotation = Quaternion.Lerp(transform.rotation, rotateDirection, _speed);

        transform.rotation = targetRotation;
    }
}
