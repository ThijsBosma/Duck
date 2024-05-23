using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSeedBack : Plant, IPickupable
{
    [SerializeField] private GameObject _Seed;

    public void PickUp()
    {
        if (_state == PlantState.planted)
        {
            if (PlayerData._Instance._ObjectPicedkup == 0)
            {
                Instantiate(_Seed, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public void LetGo()
    {
        
    }
}
