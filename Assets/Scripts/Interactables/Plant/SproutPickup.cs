using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SproutPickup : PickUpObject, IPickupable
{
    [SerializeField] private GameObject _seed;
    private Plant _plant;

    private void Start()
    {
        _plant = GetComponentInChildren<Plant>();
    }

    public void PickUp()
    {
        if (_plant._state == PlantState.planted)
        {
            PlayerData._Instance._ObjectPickedup = 0;
            Instantiate(_seed, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void LetGo()
    {
    }
}
