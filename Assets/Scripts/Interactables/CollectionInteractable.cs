using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionInteractable : MonoBehaviour, IInteractable
{
    public void Interact(PlayerData playerData)
    {
        playerData._DucksCollectedInStage += 1;
        Debug.Log(playerData._DucksCollectedInStage);
    }
}
