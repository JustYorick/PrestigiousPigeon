using UnityEngine;
using UnityEngine.UI;

public class WolfAnimator : MonoBehaviour
{
    public Animator _animator;
    private Animator Animator
    {
        get
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
            return _animator;
        }
    }

    private bool IsIdle => Animator.GetBool("IsIdle");
    
    private bool IsWalking => Animator.GetBool("IsWalking");

    private bool IsHit => Animator.GetBool("IsHit");

    private bool IsWolfDead => Animator.GetBool("IsWolfDead");

    private void Start()
    {
        Animator.enabled = true; // Enable the animator if it's disabled.
    }

    private void Update()
    {
        CheckAnimations();
    }

    private void CheckAnimations()
    {
        if (IsWalking && !IsIdle)
        {
            Animator.Play("Walk");
        }
        
        if (IsHit && !IsWolfDead)
        {
            Animator.Play("takeDamage");
        }

        if (IsWolfDead && !IsIdle)
        {
            Animator.Play("Death");
        }
    }
}