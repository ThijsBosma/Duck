using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : PlayerInteract, IInteractable
{
    //private KeyCollector _keyCollector;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("CollisionDetection");

    //    if(_keyCollector != null)
    //    {
    //        Debug.Log("KeyCollector is filled");

    //        if(_keyCollector._Key > 0)
    //        {
    //            Debug.Log("Key removed");
    //            _keyCollector._Key = 1;
    //            Destroy(gameObject);
    //        }
    //    }
    //    else
    //    {
    //        _keyCollector = GetComponent<KeyCollector>();
    //        Debug.Log("Getting keycollector component");
    //    }
    //}
    public void Interact()
    {
    }

    public void UnInteract()
    {
    }
}
