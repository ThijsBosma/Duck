using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockedDoor : MonoBehaviour, IInteractable
{
    private bool _hasInteracted;

    [SerializeField] private UnityEvent OnOpenDoor;

    public void Interact()
    {
        if (PlayerData._Instance._KeyPickedup == 1)
        {
            OnOpenDoor.Invoke();
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
