using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideUI : InputHandler
{
    [SerializeField] private Transform _UIElements;

    [SerializeField] private float _waitTime;
    [SerializeField] private float _moveTime;

    [SerializeField] private AnimationCurve _curve;

    [SerializeField] private Transform _InitialPosition;
    [SerializeField] private Transform _EndPosition;

    private Coroutine _moveRoutine;

    private float time;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > _waitTime)
        {
            if (_moveRoutine == null)
            {
                //StopCoroutine(SlideOut());
                _moveRoutine = StartCoroutine(SlideIn());
            }
        }

        if (_Move.ReadValue<Vector2>() != Vector2.zero)
        {
            if (_moveRoutine != null)
            {
                //StopCoroutine(SlideIn());
                _moveRoutine = StartCoroutine(SlideOut());
                _moveRoutine = null;
            }

            time = 0;
        }
    }

    private IEnumerator SlideIn()
    {
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime / _moveTime;

            float t = _curve.Evaluate(time);

            _UIElements.position = Vector3.Lerp(_InitialPosition.position, _EndPosition.position, t);
            yield return null;
        }
    }

    private IEnumerator SlideOut()
    {
        float time = 0;

        Vector3 _startPosition = _UIElements.position;

        while (time < 1)
        {
            time += Time.deltaTime / _moveTime;

            float t = _curve.Evaluate(time);

            _UIElements.position = Vector3.Lerp(_startPosition, _InitialPosition.position, t);
            yield return null;
        }
    }
}
