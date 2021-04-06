using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireState : StateMachineBehaviour
{
    [SerializeField] private float _fireTimer = 3f;
    private float _originalTimer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _originalTimer = _fireTimer;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FireTimer();

        if (_fireTimer <= 0)
        {
            animator.SetTrigger("Walk");
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _fireTimer = _originalTimer;
        animator.ResetTrigger("Walk");
    }
    private void FireTimer()
    {
        _fireTimer -= Time.deltaTime;
        if (_fireTimer <= 0)
        {
            _fireTimer = 0;
        }
    }
}
