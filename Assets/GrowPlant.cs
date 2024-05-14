using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlant : FindInputBinding, IInteractable
{
    [SerializeField] private GameObject _PlantToGrow;
    [SerializeField] private GameObject _BridgeToSpawn;

    [SerializeField] private Transform _SpawnPoint;
    [SerializeField] private Transform _BridgePosition;

    private bool canInteract;
    private bool treePlanted;

    private void Update()
    {
        if (canInteract && !treePlanted && _Interact.IsPressed())
        {
            InstantiatePlant();

            canInteract = false;
            treePlanted = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !treePlanted)
        {
            _Interact.Enable();
            canInteract = true;
            Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _Interact.Disable();

            if(!treePlanted)
                InteractText.instance.ResetText();
        }
    }

    public void Interact()
    {
        InteractText.instance.SetText($"Press {FindBinding()} to pick up");
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

        plant.GetComponent<PushTree>()._BridgePosition = _BridgePosition;
        plant.GetComponent<PushTree>()._BridgToSpawn = _BridgeToSpawn;

        InteractText.instance.ResetText();
        this.enabled = false;

    }

    public void UnInteract()
    {
    }
}
