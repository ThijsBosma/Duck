using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlant : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _PlantToGrow;

    [SerializeField] private Transform _SpawnPoint;

    private bool treePlanted;
    private bool _hasInteracted;

    public void Interact()
    {
        if (!treePlanted)
        {
            if (PlayerData._Instance._WateringCanPickedup == 1 && PlayerData._Instance._WateringCanHasWater == 1)
            {
                InstantiatePlant();

                PlayerData._Instance._WateringCanHasWater = 0;

                treePlanted = true;
            }
            else if(PlayerData._Instance._WateringCanHasWater == 0)
            {
                InteractText.instance.SetText("Wateringcan needs water ");
                Debug.LogError("Watering can has no water");
            }
            else
            {
                InteractText.instance.SetText("Needs water");
                Debug.LogError("Player is not holding a watering can");
            }
        }
    }

    public void InstantiatePlant()
    {
        StartCoroutine(PlantGrowth());
    }

    private IEnumerator PlantGrowth()
    {
        if (_PlantToGrow != null)
        {
            InteractText.instance.SetText($"Growing {_PlantToGrow.name}");
            yield return new WaitForSeconds(1.5f);

            _PlantToGrow.SetActive(true);

            //Spawns in direction of local green arrow of spawnpoint
            _PlantToGrow.GetComponentInChildren<Animator>().Play("Grow");

            _PlantToGrow.transform.rotation = transform.rotation;
            _PlantToGrow.transform.SetParent(transform);

            InteractText.instance.ResetText();
            _hasInteracted = true;
        }
        else
        {
            Debug.LogWarning("Plant to grow field can't be null");
        }
    }

    public void UnInteract()
    {

    }

    public bool HasInteracted()
    {
        InteractText.instance.ResetText();
        return _hasInteracted;
    }
}
