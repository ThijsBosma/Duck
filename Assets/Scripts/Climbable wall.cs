using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbablewall : MonoBehaviour, IInteractable
{
    private ThirdPersonController _controller;

    public void Interact()
    {
        _controller._IsClimbing = true;
    }

    public void UnInteract()
    {
        throw new System.NotImplementedException();
    }

    public bool HasInteracted()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        _controller = FindObjectOfType<ThirdPersonController>();
    }

}
