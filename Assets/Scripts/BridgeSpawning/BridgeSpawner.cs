using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSpawner : MonoBehaviour
{
    [SerializeField] public Transform _BridgePosition;
    [SerializeField] public GameObject _BridgToSpawn;

    [SerializeField] private Vector3 _offset;

    [Header("Particles Settings")]
    [SerializeField] private GameObject _Particles;
    [SerializeField] private Vector3 _particleOffset;

    private Plant _plant;

    private void Start()
    {
        _plant = GetComponentInParent<Plant>();
    }

    /// <summary>
    /// Called by the animator
    /// </summary>
    public void SpawnBridge()
    {
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        Vector3 offsetParticle = _plant._grid.GetParticleOffsetPosition(_plant._GridPosition.x, _plant._GridPosition.y);
        Debug.Log(offsetParticle);

        GameObject particle = Instantiate(_Particles, _particleOffset, Quaternion.identity, transform);

        particle.transform.localPosition = offsetParticle;
        
        yield return new WaitForSeconds(0.3f);

        GameObject bridge = Instantiate(_BridgToSpawn);

        Transform bridgePivot = bridge.GetComponentsInChildren<Transform>()[1];

        bridge.transform.position = _plant._grid.GetWorldPosition(_plant._GridPosition.x, _plant._GridPosition.y);

        Vector3 offsetPosition = _plant._grid.GetBridgeOffsetPosition(_plant._GridPosition.x, _plant._GridPosition.y);
        Quaternion offsetRotation = _plant._grid.GetBridgeOffsetRotation(_plant._GridPosition.x, _plant._GridPosition.y);

        if (offsetPosition != Vector3.zero && offsetRotation != Quaternion.identity)
        {
            bridgePivot.localPosition = new Vector3(bridgePivot.localPosition.x, offsetPosition.y, offsetPosition.z);
            bridgePivot.rotation = Quaternion.Euler(bridgePivot.eulerAngles.x, offsetRotation.eulerAngles.y, bridgePivot.eulerAngles.z);
        }

        yield return new WaitForSeconds(3f);

        //Destroy(transform.parent.parent.parent.gameObject);
    }
}
