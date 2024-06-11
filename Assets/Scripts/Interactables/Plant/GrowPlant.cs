using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlant : Plant, IInteractable
{
    [SerializeField] private GameObject _PlantToGrow;

    [SerializeField] private Transform _SpawnPoint;

    private Collider _treeCollider;
    private Animator _animator;

    private bool canGrow;
    private bool _hasInteracted;

    private void Start()
    {
        _treeCollider = GetComponentInChildren<CapsuleCollider>();

        _animator = GetComponentInChildren<Animator>();
    }

    public void Interact()
    {
        if (_state == PlantState.planted)
        {
            if (PlayerData._Instance._WateringCanPickedup == 1 && PlayerData._Instance._WateringCanHasWater == 1)
            {
                InstantiatePlant();

                PlayerData._Instance._WateringCanHasWater = 0;

                canGrow = true;
                _hasInteracted = true;
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
            _state = PlantState.growing;
            _treeCollider.isTrigger = false;

            //Spawns in direction of local green arrow of spawnpoint
            _animator.Play("Grow");
            yield return new WaitForSeconds(1.5f);

            _state = PlantState.grown;
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
        if (_state == PlantState.grown)
        {
            InteractText.instance.ResetText();
        }
        return _hasInteracted;
    }
}
