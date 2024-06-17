using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWater : MonoBehaviour,IInteractable
{
    //Need this in order to enable and disable the script but have no use for it so thats why its empty 
    private void Start()
    {
        
    }


    public bool HasInteracted()
    {
        return false;
    }

    public void Interact()
    {
        if (PlayerData._Instance._WateringCanPickedup == 1 && PlayerData._Instance._WateringCanHasWater == 0)
        {
            PlayerData._Instance._WateringCanHasWater = 1;
        }
        else if (PlayerData._Instance._WateringCanHasWater == 1)
        {
            InteractText.instance.SetText("Watering can already filled");
        }
        else if (PlayerData._Instance._WateringCanPickedup == 0)
        {
            Debug.LogError("Player has no watering can picked up");
        }
    }

    public void UnInteract()
    {

    }
}
