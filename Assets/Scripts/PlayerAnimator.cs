using System;
using CombatMenu;
using ReDesign;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimator : MonoBehaviour
{
    public static Animator _animator;
    private SpellMenu _spellMenu;
    private ActionButton _spellsButton;
    private ActionButton _moveButton;

    private Animator Animator
    {
        get
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
            return _animator;
        }
    }

    private Animator _spellBookAnimator;
    private GameObject _spellBook;

    private bool IsWalking => Animator.GetBool("isWalking");

    private bool IsSpellMenuEnabled => _spellMenu.IsOpen;

    private bool IsFireCasted => Animator.GetBool("fireCasted");

    private bool IsIceCasted => Animator.GetBool("iceCasted");

    private bool IsHit => Animator.GetBool("isHit");

    private bool IsPlayerDead => Animator.GetBool("PlayerDead");

    private bool IsScrolling => Animator.GetBool("isScrolling");


    private void Awake()
    {
        _spellBook = GameObject.Find("spellbook");
        _spellBookAnimator = _spellBook.GetComponent<Animator>();
    }

    private void Start()
    {
        _spellMenu = GameObject.Find("SpellMenu").GetComponent<SpellMenu>();
        _spellsButton = GameObject.Find("SpellButton").GetComponent<ActionButton>();
        _moveButton = GameObject.Find("MovementButton").GetComponent<ActionButton>();


        Animator.enabled = true; // Enable the animator if it's disabled.
    }

    private void Update()
    {
        CheckAnimations();
    }

    private void CheckAnimations()
    {
        if (IsWalking)
        {
            Animator.Play("Walking");

        }
        
        if (IsSpellMenuEnabled && !IsWalking)
        {
            Animator.SetBool("isScrolling", true);

            if (!IsFireCasted || !IsIceCasted)
            {
                _spellBookAnimator.SetBool("spellBookOpen", true);
            }
        }
        else
        {
            Animator.SetBool("isScrolling", false);
        }
        
        if (IsScrolling)
        {
            _spellBookAnimator.SetBool("spellBookOpen", true); 
        }
        else
        {
            _spellBookAnimator.SetBool("spellBookOpen", false);
        }

        if (IsFireCasted)
        {
            Animator.SetBool("hasCasted", false);
            Animator.Play("Fire Spell");

            if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Fire Spell") && 
                Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 && 
                !Animator.IsInTransition(0))
            {
                ChangeButton(false);
                _spellBookAnimator.SetBool("spellBookOpen", false);
            }
            
            else if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Fire Spell") &&
                     Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                Animator.SetBool("fireCasted", false);
                Animator.SetBool("hasCasted", true);
                ChangeButton(true);
                _spellMenu.AllowedToOpen = true;
                Animator.SetBool("isScrolling", false);
            }
        }
        else if (IsIceCasted)
        {
            Animator.SetBool("hasCasted", false);
            Animator.Play("Ice Spell");
            if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Ice Spell") &&
                Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 &&
                !Animator.IsInTransition(0))
            {
                ChangeButton(false);
                _spellBookAnimator.SetBool("spellBookOpen", false);
            }
            else if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 &&
                     Animator.GetCurrentAnimatorStateInfo(0).IsName("Ice Spell"))
            {
                Animator.SetBool("iceCasted", false);
                ChangeButton(true);
                _spellMenu.AllowedToOpen = true;
                Animator.SetBool("isScrolling", false);
                Animator.SetBool("hasCasted", true);
            }
        }

        if (IsHit && !IsPlayerDead)
        {
            Animator.Play("TakeDamage");
            if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                if (!IsPlayerDead)
                {
                    Animator.SetBool("isHit", false);
                }
            }
        }

        if (IsPlayerDead)
        {
            Animator.Play("Death");
        }
        
        if (TurnController._turnPart == 1)
        {
            _spellsButton.button.interactable = true;
            _moveButton.button.interactable = true;
        }
        if (TurnController._turnPart == 0 || TurnController._turnPart > 1)
        {
            _spellsButton.button.interactable = false;
            _moveButton.button.interactable = false;
        }
    }

    private void ChangeButton(bool status)
    {
        _spellsButton.button.interactable = status;
        _moveButton.button.interactable = status;
        if(status){
            _moveButton.Activate();
        }
    }

    public static bool PerformingAction()
    {
        return _animator.GetBool("isWalking") || _animator.GetBool("iceCasted") || _animator.GetBool("fireCasted");
    } 
}