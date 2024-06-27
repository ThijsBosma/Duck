using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbablewall : InputHandler
{
    private ThirdPersonController _controller;

    private void Start()
    {
        _controller = FindObjectOfType<ThirdPersonController>();
        _controller._WallHeight = transform.lossyScale;
    }

    private void Update()
    {
        if(_controller._IsClimbing)
        {
            _controller._WallRight = transform.right;
            _controller._WallUp = transform.up;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _controller._IsClimbing = true;
            Debug.Log("Enabled");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //_controller._IsClimbing = false;
        }
    }
}
