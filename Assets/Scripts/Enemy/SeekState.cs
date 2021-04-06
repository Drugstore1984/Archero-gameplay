using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekState : StateMachineBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _fireRange = 5f;
    [SerializeField] private float _seekTimer = 5f;
    private float _originalTimer;
    Transform _player;
    Rigidbody _rigidbody;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _originalTimer = _seekTimer;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rigidbody = animator.GetComponent<Rigidbody>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SeekTimer();
        if (_player.gameObject.activeSelf)
        {
            Vector3 moveDirection = (_player.transform.position - _rigidbody.transform.position).normalized * _speed;
            float distance = Vector3.Distance(_player.transform.position, _rigidbody.transform.position);
            _rigidbody.velocity = moveDirection;

            if (distance <= _fireRange && _seekTimer <= 0)
            {
                animator.SetTrigger("Fire");
                _rigidbody.velocity = new Vector3(0, 0, 0);
            }
        }
        else
        {
            _rigidbody.velocity = new Vector3(0, 0, 0);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _seekTimer = _originalTimer;
        animator.ResetTrigger("Fire");
        _rigidbody.velocity = new Vector3(0, 0, 0);
    }
    private void SeekTimer()
    {
        _seekTimer -= Time.deltaTime;
        if (_seekTimer <= 0)
        {
            _seekTimer = 0;
        }
    }
}
