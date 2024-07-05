using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillWaterUI : MonoBehaviour
{
    [SerializeField] private Transform _Fill;
    [SerializeField] private GameObject[] _WaterBuckets; // 0 is empty bucket, 1 is filled bucket

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
            time += Time.deltaTime / _FillTime;

            float t = _curve.Evaluate(time);

            float yScale = Mathf.Lerp(0, 1, t);

            _Fill.transform.localScale = new Vector3(1, yScale, 1);
            yield return null;
        }

        _WaterBuckets[1].SetActive(true);
        _WaterBuckets[0].SetActive(false);
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

        _WaterBuckets[1].SetActive(false);
        _WaterBuckets[0].SetActive(true);
    }
}
