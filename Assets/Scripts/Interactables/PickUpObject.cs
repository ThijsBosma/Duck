using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public bool _grabbed;

    public Transform _HoldPosition;
    public Transform _PickupPosition;

    public Collider _Collider;
    
    public Rigidbody _Rb;

    [SerializeField] protected Vector3 _offsePosition;
    [SerializeField] protected Quaternion _offsetRotation;

    protected bool _hasInteracted;

    private void Update()
    {
        if (_grabbed)
        {
            transform.position = _HoldPosition.position + _offsePosition;
            transform.rotation = _HoldPosition.rotation * _offsetRotation;
        }
    }
}
