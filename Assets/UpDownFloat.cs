using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownFloat : MonoBehaviour
{
    [SerializeField] private float _Speed;

    [SerializeField] private float _Frequency;
    [SerializeField] private float _Magnitude;

    [SerializeField] private float _offset;

    private Vector3 _startPosition;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0, _Speed * Time.deltaTime * 10f, 0);

        float sinY = Mathf.Sin(Time.time * _Frequency) * _Magnitude;

        transform.position = _startPosition + transform.up * Mathf.Sin(Time.time * _Frequency + _offset) * _Magnitude;
    }
}
