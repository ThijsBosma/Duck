using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillWaterUI : MonoBehaviour
{
    [SerializeField] private Transform _Fill;

    [SerializeField] private float _FillTime;

    [SerializeField] private AnimationCurve _curve;

    public void FillUpUI()
    {
        StartCoroutine(FillUp());
    }

    public void FillOutUI()
    {
        StartCoroutine(FillOut());
    }

    private IEnumerator FillUp()
    {
        float time = 0;

        while(time < 1)
        {
            Debug.Log("Filling up");
            time += Time.deltaTime / _FillTime;

            float t = _curve.Evaluate(time);

            float yScale = Mathf.Lerp(0, 1, t);
            Debug.Log($"yScale : {yScale}");

            _Fill.transform.localScale = new Vector3(1, yScale, 1);
            yield return null;
        }
    }

    private IEnumerator FillOut()
    {
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime / _FillTime;

            float t = _curve.Evaluate(time);

            float yScale = Mathf.Lerp(1, 0, t);

            _Fill.transform.localScale = new Vector3(1, yScale, 1);
            yield return null;
        }
    }
}
