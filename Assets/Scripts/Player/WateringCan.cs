using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : PickUpObject, IPickupable
{
    public void PickUp()
    {
        _grabbed = true;

        PlayerData._Instance._WateringCanPickedup = 1;

        StartCoroutine(LerpToHoldPosition());

        _hasInteracted = true;
    }

    public void LetGo()
    {
        _grabbed = false;

        PlayerData._Instance._WateringCanPickedup = 0;

        ChangeInputIcons._Instance._changeInteractTo = "Interact";

        StartCoroutine(LerpToPickupPostion());

        _hasInteracted = false;
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
