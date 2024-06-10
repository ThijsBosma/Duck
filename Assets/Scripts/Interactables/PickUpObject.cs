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
    protected bool _coroutineFinished;
    private Coroutine _lerpCoroutine;

    private void Update()
    {
        if (_grabbed)
        {
            if (_lerpCoroutine == null && _coroutineFinished == false)
            {
               _lerpCoroutine = StartCoroutine(LerpToHoldPosition());
            }
        }
    }

    protected IEnumerator LerpToHoldPosition()
    {
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime / _Time;

            float t = _Curve.Evaluate(time);

            transform.position = Vector3.Lerp(transform.position, _HoldPosition.position + _offsePosition, t);
            transform.rotation = Quaternion.Slerp(transform.rotation, _HoldPosition.rotation * _offsetRotation, t);
            yield return null;
        }

        _coroutineFinished = true;
        _lerpCoroutine = null;
    }
}
