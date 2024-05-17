using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbablewall : MonoBehaviour, IInteractable
{
    private ThirdPersonController _controller;

    private bool _yes;

    void Start()
    {
        _controller = FindObjectOfType<ThirdPersonController>();
    }

    public void Interact()
    {
        _controller._IsClimbing = true;
    }

    public void UnInteract()
    {
        _controller._IsClimbing = false;
    }

    public bool HasInteracted()
    {
        return _yes;
    }
}
