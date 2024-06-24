using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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

    [Header("Lerp")]
    [SerializeField] private AnimationCurve _Curve;
    [SerializeField] private float _Time;

    private Coroutine _pickupCoroutine;
    protected Coroutine _dropDownCoroutine;
    protected bool _isPickupCoroutineFinished;

    private int _timesPressed;

    private void Update()
    {
        if (_grabbed)
        {
            transform.position = _HoldPosition.position + _offsePosition;
        }
    }

    /// <summary>
    /// Goes from the transform of the gameobject to the holdposition
    /// </summary>
    /// <returns></returns>
    protected IEnumerator LerpToHoldPosition()
    {
        _dropDownCoroutine = null;

        _Collider.isTrigger = true;

        _hasInteracted = true;

        _Rb.isKinematic = true;
        _Rb.useGravity = false;
        _Rb.mass = 0;

        transform.SetParent(_HoldPosition);

        float time = 0;

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (time < 1)
        {
            time += Time.deltaTime / _Time;

            float t = _Curve.Evaluate(time);

            transform.position = Vector3.Lerp(startPosition, _HoldPosition.position + _offsePosition, t);
            transform.rotation = Quaternion.Slerp(startRotation, _HoldPosition.rotation * _offsetRotation, t);
            yield return null;
        }

        _grabbed = true;

        _isPickupCoroutineFinished = true;
    }

    /// <summary>
    /// Goes from the hold position to the pickup position
    /// </summary>
    /// <returns></returns>
    protected IEnumerator LerpToPickupPostion()
    {
        _pickupCoroutine = null;

        float time = 0;
        Vector3 startPostion = transform.position;
        Quaternion startRotation = transform.rotation;
            
        while (time < 1)
        {
            time += Time.deltaTime / _Time;

            float t = _Curve.Evaluate(time);

            transform.position = Vector3.Lerp(startPostion, _PickupPosition.position, t);
            transform.rotation = Quaternion.Slerp(startRotation, _PickupPosition.rotation, t);
            yield return null;
        }

        _grabbed = false;

        _Collider.isTrigger = false;

        _Rb.isKinematic = false;
        _Rb.useGravity = true;
        _Rb.mass = 10;

        transform.SetParent(null);

        _isPickupCoroutineFinished = false;
    }
}   
