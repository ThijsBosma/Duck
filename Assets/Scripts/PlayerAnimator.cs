using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : InputHandler
{
    [SerializeField] private Animator _PlayerAnimator;
    private bool _isPickingUp;

    private Vector2 _movementValue;

    private void Start()
    {
        _PlayerAnimator.SetInteger("PlayerState", 0);
    }

    private void Update()
    {
        _movementValue = _Move.ReadValue<Vector2>();

        if(_movementValue.magnitude > 0.5f && !_isPickingUp)
        {
            _PlayerAnimator.SetInteger("PlayerState", 1);
        }
        else if(_movementValue.magnitude < 0.5f && !_isPickingUp)
        {
            _PlayerAnimator.SetInteger("PlayerState", 0);
        }

        //if(_Pickup.WasPressedThisFrame())
        //{
        //    _isPickingUp = true;
        //    _PlayerAnimator.SetInteger("PlayerState", 2);
        //}
    }
}
