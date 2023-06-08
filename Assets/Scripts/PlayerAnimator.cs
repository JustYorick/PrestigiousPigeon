using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimator : MonoBehaviour
{
    public static Animator _animator;
    private SpellMenu _spellMenu;
    private Button _spellsButton;
    private Button _moveButton;

    private Animator Animator
    {
        get
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
            return _animator;
        }
    }

    private bool IsWalking => Animator.GetBool("isWalking");

    private bool IsSpellMenuEnabled => _spellMenu.IsOpen;

    private bool IsFireCasted => Animator.GetBool("fireCasted");

    private bool IsIceCasted => Animator.GetBool("iceCasted");

    private bool IsHit => Animator.GetBool("isHit");

    private bool IsPlayerDead => Animator.GetBool("PlayerDead");

    private void Start()
    {
        _spellMenu = GameObject.Find("SpellMenu").GetComponent<SpellMenu>();
        _spellsButton = GameObject.Find("SpellButton").GetComponent<Button>();
        _moveButton = GameObject.Find("MovementButton").GetComponent<Button>();

        
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
            Animator.Play("Scrolling");
        }
        else
        {
            Animator.SetBool("isScrolling", false);
        }

        if (IsFireCasted)
        {
            ChangeButton(false);
            Animator.SetBool("hasCasted", false);
            Animator.Play("Fire Spell");
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                Animator.SetBool("fireCasted", false);
                ChangeButton(true);
                _spellMenu.OpenIfActivated();
            }
            Animator.SetBool("hasCasted", true);
        }
        else if (IsIceCasted)
        {
            ChangeButton(false);
            Animator.SetBool("hasCasted", false);
            Animator.Play("Ice Spell");
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                Animator.SetBool("iceCasted", false);
                ChangeButton(true);
                _spellMenu.OpenIfActivated();
            }
            Animator.SetBool("hasCasted", true);
        }

        if (IsHit && !IsPlayerDead)
        {
            Animator.Play("TakeDamage");
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
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
    }

    private void ChangeButton(bool status)
    {
        _spellsButton.interactable = status;
        _moveButton.interactable = status;
    }
}