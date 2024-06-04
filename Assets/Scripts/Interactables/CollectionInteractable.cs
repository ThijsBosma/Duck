using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionInteractable : MonoBehaviour, IPlayerData
{
    [SerializeField] private int _ID;

    public void CollectDuck(PlayerData playerData)
    {
        playerData._DucksCollectedInStage += 1;
        PlayerData._Instance._DuckIDs.Add(_ID);

        Debug.Log(playerData._DucksCollectedInStage);
    }
}
