using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerControls _Input;

    protected PlayerInput playerInput;

    protected InputAction _Move;
    protected InputAction _Look;
    protected InputAction _Interact;
    protected InputAction _GrowPlant;

    protected virtual void Awake()
    {
        _Input = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _Move = _Input.Player.Move;
        _Move.Enable();

        _Look = _Input.Player.Look;
        _Look.Enable();

        _Interact = _Input.Player.Interact;

        _GrowPlant = _Input.Player.GrowPlant;
    }

    private void OnDisable()
    {
        _Move.Disable();
        _Look.Disable();
        _Interact.Disable();
    }

    protected void DisableInput()
    {
        _Move.Disable();
        _Look.Disable();
        _Interact.Disable();
    }
}
