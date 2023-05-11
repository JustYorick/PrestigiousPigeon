using System;
using System.Linq;
using ReDesign;
using ReDesign.Entities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimator : MonoBehaviour
{
    public static Animator _animator;
    [SerializeField] private Canvas spellMenu;
    [SerializeField] private Button spellsButton;
    [SerializeField] private Button moveButton;

    
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
        }

        if (spellMenu.enabled)
        {
            _animator.SetBool("isScrolling", true);
            _animator.Play("Scrolling");
        }
        else if (!spellMenu.enabled)
        {
            _animator.SetBool("isScrolling", false);
        }

        if (_animator.GetBool("fireCasted"))
        {
            spellsButton.interactable = false;
            moveButton.interactable = false;
            _animator.SetBool("hasCasted", false);
            _animator.Play("Fire Spell");
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                _animator.SetBool("fireCasted", false);
                _animator.SetBool("hasCasted", true);
                spellsButton.interactable = true;
                moveButton.interactable = true;
            }
        }

        if (_animator.GetBool("iceCasted"))
        {
            spellsButton.interactable = false;
            moveButton.interactable = false;
            _animator.SetBool("hasCasted", false);
            _animator.Play("Ice Spell");
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                _animator.SetBool("iceCasted", false);
                _animator.SetBool("hasCasted", true);
                spellsButton.interactable = true;
                moveButton.interactable = true;
            }
        }

        if (_animator.GetBool("isHit"))
        {
            _animator.Play("TakeDamage");
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                _animator.SetBool("isHit", false);
            }
        }

        if (_animator.GetBool("PlayerDead"))
        {
            _animator.Play("Death");
        }
    }
}