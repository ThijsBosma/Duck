using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlant : InputHandler, IInteractable
{
    [SerializeField] private GameObject plantToGrow;

    [SerializeField] private Transform spawnPoint;

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
            InteractText.instance.ResetText();
        }
    }

    public void Interact()
    {
        InteractText.instance.SetText("Press F to grow plant");
    }

    public void InstantiatePlant()
    {
        StartCoroutine(PlantGrowth());
    }

    private IEnumerator PlantGrowth()
    {
        InteractText.instance.SetText($"Growing {plantToGrow.name}");
        yield return new WaitForSeconds(1.5f);

        Instantiate(plantToGrow, spawnPoint.position, Quaternion.identity);
        InteractText.instance.ResetText();

    }
}
