using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinUpAndDown : MonoBehaviour
{
    [SerializeField] private float _Frequency;
    [SerializeField] private float _Magnitude;
    [SerializeField] private float _Yoffset;

    private PickUpObject _SeedPickup;
    private Rigidbody _Rb;

    private Vector3 _startSize;

    private void Start()
    {
        _startSize = transform.localScale;

        _SeedPickup = GetComponent<PickUpObject>();
        _Rb = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if (!_SeedPickup._grabbed)
        {
            if (_Rb.useGravity)
                _Rb.useGravity = false;

            float sin = Mathf.Sin(Time.time * _Frequency + _Yoffset) * _Magnitude;

            transform.position = new Vector3(transform.position.x, transform.position.y + (sin / 1000f), transform.position.z);
        }
    }
}
