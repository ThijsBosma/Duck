using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinGrowthAndShrink : MonoBehaviour
{
    [SerializeField] private float _Frequency;
    [SerializeField] private float _Magnitude;

    private Vector3 _startSize;

    private void Start()
    {
        _startSize = transform.localScale;
    }

    private void LateUpdate()
    {
        float sin = Mathf.Sin(Time.time * _Frequency) * _Magnitude;

        transform.localScale = _startSize + (_startSize * sin);
    }
}
