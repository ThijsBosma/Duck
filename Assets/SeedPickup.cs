using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPickup : PickUpObject, IPickupable
{
    public void PickUp()
    {
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
        _grabbed = false;

        _hasInteracted = false;

        _Collider.isTrigger = false;

        _Rb.isKinematic = false;
        _Rb.useGravity = true;
        _Rb.mass = 10;

        transform.position = _PickupPosition.position;
        transform.SetParent(null);
    }
}
