using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBox : MonoBehaviour, IPickupable
{
    public bool _grabbed;

    [SerializeField] private Transform _HoldPosition;
    [SerializeField] private Transform _PickupPosition;

    private BoxCollider _Collider;
    private Rigidbody _Rb;

    private bool _hasInteracted;

    private void Start()
    {
        _Collider = GetComponent<BoxCollider>();
        _Rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_grabbed)
        {
            transform.position = _HoldPosition.position;
            transform.rotation = _HoldPosition.rotation;
        }
    }

    private void OnValidate()
    {
        _Collider = GetComponent<BoxCollider>();
        _Rb = GetComponent<Rigidbody>();
    }

    public void PickUp()
    {
        _grabbed = true;

        _Collider.isTrigger = true;

        _hasInteracted = true;

        _Rb.isKinematic = true;
        _Rb.useGravity = false;
        _Rb.mass = 0;

        transform.SetParent(_HoldPosition);
    }

    public void LetGo()
    {
        _grabbed = false;

        _hasInteracted = false;

        _Collider.isTrigger = false;

        _Rb.isKinematic = false;
        _Rb.useGravity = true;
        _Rb.mass = 10;

        transform.position = _PickupPosition.position;
        transform.SetParent(null);
    }
}
