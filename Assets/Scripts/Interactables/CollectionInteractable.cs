using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionInteractable : MonoBehaviour, IPlayerData
{
    public void CollectDuck(PlayerData playerData)
    {
        playerData._DucksCollectedInStage += 1;
        Debug.Log(playerData._DucksCollectedInStage);
    }
}
