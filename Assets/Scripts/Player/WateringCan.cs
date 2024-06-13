using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : PickUpObject, IPickupable
{
    public void PickUp()
    {
        _grabbed = true;

        PlayerData._Instance._WateringCanPickedup = 1;

        _Collider.isTrigger = true;

        _hasInteracted = true;

        _Rb.isKinematic = true;
        _Rb.useGravity = false;
        _Rb.mass = 0;

        transform.SetParent(_HoldPosition);
    }

    public void LetGo()
    {
        if (_dropDownCoroutine == null && _isPickupCoroutineFinished)
        {
            _dropDownCoroutine = StartCoroutine(LerpToPickupPostion());
        }

        _grabbed = false;

        PlayerData._Instance._WateringCanPickedup = 0;

        _hasInteracted = false;

        _Collider.isTrigger = false;

        _Rb.useGravity = true;
        _Rb.mass = 10;

        _Rb.isKinematic = true;
        transform.position = _PickupPosition.position;
        transform.SetParent(null);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<GetWater>() != null)
        {
            FillWateringCan();
        }
    }

    private void FillWateringCan()
    {
        PlayerData._Instance._WateringCanHasWater = 1;
    }

}
