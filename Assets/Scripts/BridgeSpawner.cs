using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSpawner : MonoBehaviour
{
    [SerializeField] public Transform _BridgePosition;
    [SerializeField] public GameObject _BridgToSpawn;

    [SerializeField] private Vector3 _offset;

    private Plant _plant;

    private void Start()
    {
        _plant = GetComponentInParent<Plant>();
    }

    public void SpawnBridge()
    {
        GameObject bridge = Instantiate(_BridgToSpawn);

        Transform bridgePivot = bridge.GetComponentsInChildren<Transform>()[1];

        Debug.Log(bridgePivot.name);

        _plant._grid.GetXZ(transform.parent.parent.parent.position, out int x, out int z);
        bridge.transform.position = _plant._grid.GetWorldPosition(x, z);
        Vector3 offset = _plant._grid.GetBridgePosition(transform.parent.parent.parent.position);
        Debug.Log(offset);

        if (offset != Vector3.zero)
        {
            bridgePivot.position += _offset;
        }

        Destroy(gameObject);
    }
}
