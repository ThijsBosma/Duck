using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent _OnLeverPull;

    [SerializeField] private Animator _InteractAnimation;

    private bool _hasInteracted;

    public bool HasInteracted()
    {
        return _hasInteracted;
    }

    public void Interact()
    {
        _InteractAnimation.SetBool("IsPressed", true);
        _hasInteracted = true;
    }

    public void SpawnObject()
    {
        _OnLeverPull.Invoke();
    }

    public void UnInteract()
    {

    }
}
