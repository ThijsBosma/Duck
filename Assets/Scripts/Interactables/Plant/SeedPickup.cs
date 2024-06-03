using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPickup : PickUpObject, IPickupable
{
    private void Start()
    {
        Debug.Log("Spawned");
        _HoldPosition = GameObject.Find("HoldPos").transform;
        _PickupPosition = GameObject.Find("PickupPos").transform;
    }

    public void PickUp()
    {
        PlayerData._Instance._SeedPickedup = 1;

        _grabbed = true;

        _Collider.isTrigger = true;

        _hasInteracted = true;

        _Rb.isKinematic = true;
        _Rb.useGravity = false;
        _Rb.mass = 0;

        transform.SetParent(_HoldPosition);
    }

    public void LetGo()
    {
        PlayerData._Instance._SeedPickedup = 0;

        _grabbed = false;

        _hasInteracted = false;

        _Collider.isTrigger = false;

        _Rb.isKinematic = false;
        _Rb.useGravity = true;
        _Rb.mass = 10;

        transform.position = _PickupPosition.position;
        transform.SetParent(null);
    }

    private void OnDestroy()
    {
        PlayerData._Instance._SeedPickedup = 0;
    }
}
