using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    private const string VectorHorizontal = "Horizontal";
    private const string VectorVertical = "Vertical";

    [SerializeField] private float _speed = 0.1f;
    
    private CharacterController _characterController;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        SetMoveDirection();
        Move();
    }

    private void SetMoveDirection()
    {
        _moveDirection.x = Input.GetAxisRaw(VectorHorizontal);
        _moveDirection.z = Input.GetAxisRaw(VectorVertical);
        _moveDirection = _moveDirection.normalized;

        _moveDirection.y = -1f;
    }

    private void Move()
    {
        _characterController.Move(_moveDirection * _speed);
    }
}
