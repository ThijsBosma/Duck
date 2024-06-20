using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideUI : InputHandler
{
    [SerializeField] private Transform _UIElements;

    [SerializeField] private float _waitTime;
    [SerializeField] private float _moveTime;

    [SerializeField] private AnimationCurve _curve;

    private Coroutine _moveRoutine;

    private float time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > _waitTime)
        {
            if (_moveRoutine == null)
            {
                StopCoroutine(SlideOut());
                _moveRoutine = StartCoroutine(SlideIn());
            }
        }

        if (_Move.ReadValue<Vector2>() != Vector2.zero)
        {
            if (_moveRoutine == null)
            {
                StopCoroutine(SlideIn());
                _moveRoutine = StartCoroutine(SlideOut());
            }

            time = 0;
        }
    }

    private IEnumerator SlideIn()
    {
        float time = 0;

        Vector3 startPosition = _UIElements.position;
        Vector3 endPosition = _UIElements.position + Vector3.right * 10;

        while (time < 1)
        {
            time += Time.deltaTime;

            float t = _curve.Evaluate(time);

            _UIElements.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
    }

    private IEnumerator SlideOut()
    {
        float time = 0;

        Vector3 startPosition = _UIElements.position;
        Vector3 endPosition = _UIElements.position - Vector3.right * 10;

        while (time < 1)
        {
            time += Time.deltaTime;

            float t = _curve.Evaluate(time);

            _UIElements.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
    }
}
