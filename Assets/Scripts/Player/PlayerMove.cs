using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private GameObject _circle, _circlePoint;
    private Rigidbody _rigidBody;
    [SerializeField] private float _moveSpeed = 3f;
    private Vector3 _moveDirection;
    Animator _animator;
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _circle = GameObject.FindGameObjectWithTag("JoystickCircle");
        _circlePoint = GameObject.FindGameObjectWithTag("JoystickPoint");
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        _moveDirection = (_circlePoint.transform.position - _circle.transform.position).normalized;
        _rigidBody.velocity = _moveDirection * _moveSpeed;

        if (_rigidBody.velocity.x != 0)
        {
            RotateToMoveDirection();
        }
        else
        {
            _animator.SetFloat("Speed",_rigidBody.velocity.x);
        }
    }
    private void RotateToMoveDirection()
    {
        float rotateAngel = Mathf.Atan2(_moveDirection.x, _moveDirection.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(rotateAngel, Vector3.back);
        _rigidBody.rotation = Quaternion.Slerp(transform.rotation, rotation, 50 * Time.deltaTime);
        _rigidBody.freezeRotation = true;
    }
}
