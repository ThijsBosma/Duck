using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTree : MonoBehaviour, IInteractable
{
    private AnimationEvent animationEvent;
    private Animator animator;
    [SerializeField] private AnimationClip animationClip;

    private PlantState _state;

    private float _time;

    private bool _hasInteracted;

    private void Start()
    {
        animator = GetComponent<Animator>();

        _state = GetComponentInParent<Plant>()._state;
    }

    public void Interact()
    {
        if(_state == PlantState.grown)
        animator.Play("FallOver");
        _hasInteracted = true;
    }

    /// <summary>
    /// Called by animation event
    /// </summary>
    public void SetPlantStateToGrown()
    {
        _state = PlantState.grown;
    }

    public void UnInteract()
    {
    }

    public bool HasInteracted()
    {
        InteractText.instance.ResetText();
        return _hasInteracted;
    }
}
