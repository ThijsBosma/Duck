using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator _InteractAnimation;
    
    public void Interact()
    {
        _InteractAnimation.SetBool("IsPressed", true);
    }
}
