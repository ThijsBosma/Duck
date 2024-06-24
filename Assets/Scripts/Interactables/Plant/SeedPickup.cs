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

        StartCoroutine(LerpToHoldPosition());

        _grabbed = true;

        _hasInteracted = true;
    }

    public void LetGo()
    {
        PlayerData._Instance._SeedPickedup = 0;

        StartCoroutine(LerpToPickupPostion());

        _grabbed = false;

        _hasInteracted = false;
    }

    private void OnDestroy()
    {
        PlayerData._Instance._SeedPickedup = 0;
    }
}
