using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent _OnLeverPull;

    [SerializeField] private Animator _InteractAnimation;
    
    public void Interact()
    {
        _InteractAnimation.SetBool("IsPressed", true);
    }

    public void SpawnObject()
    {
        _OnLeverPull.Invoke();
    }
}
