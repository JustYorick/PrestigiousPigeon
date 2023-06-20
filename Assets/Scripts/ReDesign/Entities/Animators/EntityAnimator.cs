using System;
using UnityEngine;

public class EntityAnimator : MonoBehaviour
{
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (_animator == null) _animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckAnimations();
    }

    void CheckAnimations()
    {
        if (_animator.GetBool("isWalking"))
        {
            _animator.Play("Walking");
            _animator.SetBool("isWalking", false);
        }

        if (_animator.GetBool("isHit") && !_animator.GetBool("isDead"))
        {
            _animator.Play("Hit");
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                _animator.SetBool("isHit", false);
            }
        }

        if (_animator.GetBool("isAttacking"))
        {
            _animator.Play("Attacking");
            _animator.SetBool("isAttacking", false);
        }

        // if (_animator.GetBool("isDead"))
        // {
        //     _animator.Play("Death");
        // }
    }


    public void SetAttacking()
    {
        _animator.SetBool("isAttacking", true);
        _animator.SetBool("isIdle", true);
    }

    public void SetWalking()
    {
        _animator.SetBool("isWalking", true);
        _animator.SetBool("isIdle", true);
    }
}