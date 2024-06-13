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

    private void FixedUpdate()
    {
        if (_grabbed)
        {
            if (_pickupCoroutine == null && !_isPickupCoroutineFinished)
            {
               _pickupCoroutine = StartCoroutine(LerpToHoldPosition());
            }
        }
    }

    /// <summary>
    /// Goes from the transform of the gameobject to the holdposition
    /// </summary>
    /// <returns></returns>
    private IEnumerator LerpToHoldPosition()
    {
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

        _isPickupCoroutineFinished = true;
        _pickupCoroutine = null;
    }

    /// <summary>
    /// Goes from the hold position to the pickup position
    /// </summary>
    /// <returns></returns>
    protected IEnumerator LerpToPickupPostion()
    {
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

        _isPickupCoroutineFinished = false;
        _dropDownCoroutine = null;
    }
}   
