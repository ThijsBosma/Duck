using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbablewall : InputHandler
{
    private ThirdPersonController _controller;
    private int _buttonPresses;

    private void Start()
    {
        _controller = FindObjectOfType<ThirdPersonController>();
    }

    private void Update()
    {
        if(_Climb.WasPressedThisFrame())
        {
            _controller._IsClimbing = true;
            _controller._WallRight = transform.right;
            _controller._WallUp = transform.up;
            _buttonPresses += 1;
        }
        else if(_buttonPresses > 1)
        {
            _buttonPresses = 0;
            _controller._IsClimbing = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ThirdPersonController>() != null)
        {
            _Climb.Enable();
        }
    }
}
