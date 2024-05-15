using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    private bool _hasInteracted;

    private KeyCollector _keyCollector;

    public void Interact()
    {
        if(_keyCollector == null)
        {
            _keyCollector = FindObjectOfType<KeyCollector>();
        }

        if (_keyCollector != null)
        {

            if (_keyCollector._Key > 0)
            {
                _keyCollector._Key -= 1;
            }
        }
    }

    public void UnInteract()
    {
        Destroy(gameObject);
    }

    public bool HasInteracted()
    {
        return _hasInteracted;
    }
}
