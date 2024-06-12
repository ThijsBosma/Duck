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

        _plant._grid.GetXZ(transform.parent.parent.parent.position, out int x, out int z, false);
        bridge.transform.position = _plant._grid.GetWorldPosition(x, z);
        
        Vector3 offsetPosition = _plant._grid.GetBridgeOffsetPosition(transform.parent.parent.parent);
        Quaternion offsetRotation = _plant._grid.GetBridgeOffsetRotation(transform.parent.parent.parent);

        if (offsetPosition != Vector3.zero && offsetRotation != Quaternion.identity)
        {
            bridgePivot.localPosition = new Vector3(bridgePivot.localPosition.x, offsetPosition.y, offsetPosition.z);
            bridgePivot.rotation = Quaternion.Euler(bridgePivot.eulerAngles.x, offsetRotation.eulerAngles.y, bridgePivot.eulerAngles.z);
        }

        Destroy(transform.parent.parent.parent.gameObject);
    }
}
