using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject[] _PortalWalls;
    [SerializeField] private Transform _StartPoint;
    [SerializeField] private Transform _EndPoint;
    private ThirdPersonController _controller;

    [Header("LerpVariables")]
    [SerializeField] private float _Time;
    [SerializeField] private AnimationCurve _curve;
    private Coroutine _coroutine;
    private int _currentIndex;

    [Header("Raycast")]
    [SerializeField] private float _MaxRayCastDistance;
    [SerializeField] private float _RaycastDownOffset;

    private void Start()
    {
        _controller = FindObjectOfType<ThirdPersonController>();
    }

    private void Update()
    {
        IsIndexOutOfBounds();

        Debug.Log(_currentIndex);
        //Debug.DrawLine(_PortalWalls[_currentIndex].transform.position - new Vector3(0, _RaycastDownOffset, 0), new Vector3(_PortalWalls[_currentIndex].transform.forward.x, _PortalWalls[_currentIndex].transform.position.y, _PortalWalls[_currentIndex].transform.forward.z) - new Vector3(0, _RaycastDownOffset, 0));

        if (RaycastHasHit() && _coroutine == null)
        {
            _coroutine = StartCoroutine(TeleportPlayer());
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(_controller != null)
    //    {
    //        StartCoroutine(TeleportPlayer());
    //    }
    //}

    private IEnumerator TeleportPlayer()
    {
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime / _Time;
            float t = _curve.Evaluate(time);
            _controller.transform.position = Vector3.Lerp(_StartPoint.position, _EndPoint.position, t);

            yield return null;
        }

        Transform tempStartPosVar = _StartPoint;

        _StartPoint = _EndPoint;
        _EndPoint = tempStartPosVar;

        _currentIndex += 1;

        _coroutine = null;
    }

    private void IsIndexOutOfBounds()
    {
        if (_currentIndex >= _PortalWalls.Length)
        {
            _currentIndex = 0;
        }
    }

    private bool RaycastHasHit()
    {
        Vector3 raycastDirection = _PortalWalls[_currentIndex].transform.forward;

        Debug.DrawRay(_PortalWalls[_currentIndex].transform.position - new Vector3(0, _RaycastDownOffset, 0), raycastDirection * _MaxRayCastDistance, Color.red);
        return Physics.Raycast(_PortalWalls[_currentIndex].transform.position - new Vector3(0, _RaycastDownOffset, 0), raycastDirection, _MaxRayCastDistance);
    }
}
