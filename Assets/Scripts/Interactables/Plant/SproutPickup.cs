using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SproutPickup : PickUpObject, IPickupable
{
    [SerializeField] private GameObject _seed;
    public Plant _plant;
    public BuildGrid _grid;

    private void Start()
    {
        _plant = GetComponentInChildren<Plant>();

        SetSproutOnGrid();
    }

    public void PickUp()
    {
        if (_plant._state == PlantState.planted)
        {
            PlayerData._Instance._ObjectPickedup = 0;
            Instantiate(_seed, transform.position, Quaternion.identity);
           _grid.Setvalue(transform.position, 0);
            Destroy(transform.parent.gameObject);
        }
    }

    public void LetGo()
    {
    }

    private void SetSproutOnGrid()
    {
        if (_grid != null)
        {
            _grid.GetXZ(transform.parent.position, out int x, out int z, false);

            _grid.SetValue(x, z, 1);

            transform.parent.position = _grid.GetWorldPosition(x, z);
        }
    }
}
