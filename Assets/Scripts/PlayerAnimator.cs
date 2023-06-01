using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimator : MonoBehaviour
{
    public static Animator _animator;
    [SerializeField] private Canvas _spellMenu;
    [SerializeField] private Button _spellsButton;
    [SerializeField] private Button _moveButton;

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

    private bool IsSpellMenuEnabled => _spellMenu.enabled;

    private bool IsFireCasted => Animator.GetBool("fireCasted");

    private bool IsIceCasted => Animator.GetBool("iceCasted");

    private bool IsHit => Animator.GetBool("isHit");

    private bool IsPlayerDead => Animator.GetBool("PlayerDead");

    private void Start()
    {
        _spellMenu = GameObject.Find("SpellMenu").GetComponent<Canvas>();
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
                ChangeButton(true);

        }
        else if (IsIceCasted)
        {
            ChangeButton(false);
            Animator.SetBool("hasCasted", false);
            Animator.Play("Ice Spell");
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
                ChangeButton(true);

        }

        if (IsHit)
        {
            Animator.Play("TakeDamage");
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