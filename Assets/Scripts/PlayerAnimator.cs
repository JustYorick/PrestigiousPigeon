using System;
using ReDesign.Entities;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private static GameObject _player;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = this.GameObject();
    }

    void Update()
    {
        if (PlayerMovement.isIdle)
        {
            _animator.Play("Idle");
        }

        if (PlayerMovement.isMoving)
        {
            _animator.Play("Walking");
        }
    }
}