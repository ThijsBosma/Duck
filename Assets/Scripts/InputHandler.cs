using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerControls input;

    protected InputAction move;
    protected InputAction look;

    protected virtual void Awake()
    {
        input = new PlayerControls();
    }

    private void OnEnable()
    {
        move = input.Player.Move;
        move.Enable();

        look = input.Player.Look;
        look.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
    }

    protected void DisableInput()
    {
        move.Disable();
        look.Disable();
    }
}
