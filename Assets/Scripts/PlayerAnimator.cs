using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : InputHandler
{
    [SerializeField] private Animator _PlayerAnimator;

    private Vector2 _movementValue;

    private void Start()
    {
        _PlayerAnimator.SetInteger("PlayerState", 0);
    }

    private void Update()
    {
        _movementValue = _Move.ReadValue<Vector2>();

        _Pickup.Enable();

        if(_movementValue.magnitude > 0.5f)
        {
            _PlayerAnimator.SetInteger("PlayerState", 1);
        }
        else
        {
            _PlayerAnimator.SetInteger("PlayerState", 0);
        }

        if(_Pickup.WasPressedThisFrame())
        {
            _PlayerAnimator.SetInteger("PlayerState", 2);
        }
    }
}
