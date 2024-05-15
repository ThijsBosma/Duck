using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlant : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _PlantToGrow;
    [SerializeField] private GameObject _BridgeToSpawn;

    [SerializeField] private Transform _SpawnPoint;
    [SerializeField] private Transform _BridgePosition;

    private bool treePlanted;
    private bool _hasInteracted;

    private PushTree pushTree;

    public void Interact()
    {
        if (treePlanted)
        {
            pushTree.Interact();
            _hasInteracted = true;
        }

        if (!treePlanted)
        {
            InstantiatePlant();

            treePlanted = true;
        }
    }

    public void InstantiatePlant()
    {
        StartCoroutine(PlantGrowth());
    }

    private IEnumerator PlantGrowth()
    {
        InteractText.instance.SetText($"Growing {_PlantToGrow.name}");
        yield return new WaitForSeconds(1.5f);

        GameObject plant = Instantiate(_PlantToGrow, _SpawnPoint.position, Quaternion.identity);

        plant.transform.SetParent(transform);

        pushTree = GetComponentInChildren<PushTree>();

        pushTree._BridgePosition = _BridgePosition;
        pushTree._BridgToSpawn = _BridgeToSpawn;

        InteractText.instance.ResetText();
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
