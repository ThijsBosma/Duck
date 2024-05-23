using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public bool _grabbed;

    [SerializeField] protected Transform _HoldPosition;
    [SerializeField] protected Transform _PickupPosition;
    [SerializeField] protected Vector3 _offsePosition;
    [SerializeField] protected Quaternion _offsetRotation;

    protected Collider _Collider;
    protected Rigidbody _Rb;

    protected bool _hasInteracted;

    private void Start()
    {
        _Collider = GetComponent<Collider>();
        _Rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_grabbed)
        {
            transform.position = _HoldPosition.position + _offsePosition;
            transform.rotation = _HoldPosition.rotation * _offsetRotation;
        }
    }

    private void OnValidate()
    {
        _Collider = GetComponent<Collider>();
        _Rb = GetComponent<Rigidbody>();
    }
}
