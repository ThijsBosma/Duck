using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropper : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _WaterPrefab;
    [SerializeField] private Transform _WaterSpawnpoint;

    [Header("Variables")]
    [SerializeField] private float _TimeBetweenSpawns;
    private Coroutine _coroutine;

    private void Update()
    {
        if(_coroutine == null)
        {
            _coroutine = StartCoroutine(SpawnWater());
        }
    }

    private IEnumerator SpawnWater()
    {
        Instantiate(_WaterPrefab, _WaterSpawnpoint.position, Quaternion.identity);
        yield return new WaitForSeconds(_TimeBetweenSpawns);

        _coroutine = null;
    }
}
