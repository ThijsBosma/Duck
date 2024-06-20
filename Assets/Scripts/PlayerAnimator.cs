using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : InputHandler
{
    [SerializeField] private Animator _PlayerAnimator;
    private bool _isPickingUp;

    private Vector2 _movementValue;
    private ThirdPersonController _controller;

    private void Start()
    {
        _controller = FindObjectOfType<ThirdPersonController>();

        _PlayerAnimator.SetInteger("PlayerState", 0);
    }

    private void Update()
    {
        if (!GameManager._Instance._enableMove)
            return;

        _movementValue = _Move.ReadValue<Vector2>();

        if(_movementValue.magnitude > 0.5f && !_isPickingUp)
        {
            _PlayerAnimator.SetInteger("PlayerState", 1);
        }
        else if(_movementValue.magnitude < 0.5f && !_isPickingUp)
        {
            _PlayerAnimator.SetInteger("PlayerState", 0);
        }

        if(_controller._IsClimbing)
        {
            _PlayerAnimator.SetInteger("PlayerState", 3);
        }

        //if(_Pickup.WasPressedThisFrame())
        //{
        //    _isPickingUp = true;
        //    _PlayerAnimator.SetInteger("PlayerState", 2);
        //}
    }
}
