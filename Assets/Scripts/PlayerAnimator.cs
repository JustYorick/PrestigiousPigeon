using System;
using System.Linq;
using ReDesign;
using ReDesign.Entities;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public static Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_animator == null) _animator = GetComponent<Animator>();
        
        if (_animator.GetBool("isIdle") && !_animator.GetBool("iceCasted") && !_animator.GetBool("fireCasted") &&
            !_animator.GetBool("hasCasted") && !_animator.GetBool("isHit") &&
            !_animator.GetBool("PlayerDead"))
        {
            _animator.Play("Idle");
        }

        if (_animator.GetBool("isMoving"))
        {
            _animator.Play("Walking");
        }

        if (_animator.GetBool("fireCasted"))
        {
            _animator.SetBool("hasCasted", false);
            _animator.Play("Fire Spell");
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                _animator.SetBool("fireCasted", false);
                _animator.SetBool("hasCasted", true);
            }
        }

        if (_animator.GetBool("iceCasted"))
        {
            _animator.SetBool("hasCasted", false);
            _animator.Play("Ice Spell");
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                _animator.SetBool("iceCasted", false);
                _animator.SetBool("hasCasted", true);
            }
        }

        if (_animator.GetBool("isHit") && !_animator.GetBool("PlayerDead"))
        {
            _animator.Play("TakeDamage");
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                if (!_animator.GetBool("PlayerDead"))
                {
                    _animator.SetBool("isHit", false);
                }
            }
        }

        if (_animator.GetBool("PlayerDead"))
        {
            _animator.Play("Death");
        }
    }
}