using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTree : FindInputBinding, IInteractable
{
    [SerializeField] public Transform _BridgePosition;
    [SerializeField] public GameObject _BridgToSpawn;

    [SerializeField] private float _MoveTime;

    [SerializeField] private AnimationCurve _Curve;

    private float _time;

    private bool _hasInteracted;

    public void Interact()
    {
        Instantiate(_BridgToSpawn, _BridgePosition.position, _BridgePosition.rotation);
        _hasInteracted = true;
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
