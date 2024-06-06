using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnClimbableWall : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _ClimbableWall;
    [SerializeField] private Transform _ClimbableWallSpawnPoint;

    private bool _wallPlanted;
    private bool _hasInteracted;

    public void Interact()
    {
        if (!_wallPlanted)
        {
            if (PlayerData._Instance._WateringCanPickedup == 1 && PlayerData._Instance._WateringCanHasWater == 1)
            {
                Instantiate(_ClimbableWall, _ClimbableWallSpawnPoint.position, _ClimbableWallSpawnPoint.rotation);
                PlayerData._Instance._WateringCanHasWater = 0;

                _wallPlanted = true;
                _hasInteracted = true;
            }
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
