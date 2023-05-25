using System;
using UnityEngine;

public class SlimeAnimator : MonoBehaviour
{
    private Animator _animator;
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
        Debug.Log(_animator);
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
            Debug.Log("isWalking");
            _animator.Play("Mobs_Slime_walking");
            _animator.SetBool("isWalking", false);

        }

        if (_animator.GetBool("isHit"))
        {
            Debug.Log("isHit");

            _animator.Play("TakeDamage");
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                _animator.SetBool("isHit", false);
            }
        }
        
        if (_animator.GetBool("isAttacking"))
        {
            Debug.Log("isAttacking");

            _animator.Play("Mobs_Slime_Attack");                
            _animator.SetBool("isAttacking", false);

        }

        if (_animator.GetBool("slimeDead"))
        {
            Debug.Log("Dead");

            _animator.Play("Mobs_Slime_Death");
        }
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