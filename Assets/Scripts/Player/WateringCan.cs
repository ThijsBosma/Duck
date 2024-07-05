using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : PickUpObject, IPickupable
{
    private SinUpAndDown _SinUpAndDown;

    private void Start()
    {
        _SinUpAndDown = GetComponent<SinUpAndDown>();
    }

    public void PickUp()
    {
        _grabbed = true;

        _SinUpAndDown.enabled = true;

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

    public void EnableRigidbody()
    {
        _Rb.useGravity = true;
        _SinUpAndDown.enabled = false;
    }

    private void FillWateringCan()
    {
        PlayerData._Instance._WateringCanHasWater = 1;
    }

}
