using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTree : FindInputBinding, IInteractable
{
    [SerializeField] public Transform _BridgePosition;
    [SerializeField] public GameObject _BridgToSpawn;

    [SerializeField] private float _MoveTime;

    [SerializeField] private AnimationCurve _Curve;

    private Animator animator;

    private float _time;

    private bool _hasInteracted;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Interact()
    {
        animator.Play("FallOver");
        _hasInteracted = true;
    }

    //Called by animator
    public void SpawnBridge()
    {
        Instantiate(_BridgToSpawn, _BridgePosition.position, _BridgePosition.rotation);
        Destroy(gameObject);
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
