using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionInteractable : MonoBehaviour, IPlayerData
{
    public DuckData _duckData;

    public void CollectDuck(PlayerData playerData)
    {
        if (!playerData._DuckIDs.Contains(_duckData._ID))
        {
            playerData._DucksCollectedInStage += 1;
            PlayerData._Instance._DuckIDs.Add(_duckData._ID);

            Debug.Log(playerData._DucksCollectedInStage);
        }
    }
}

[System.Serializable]
public class DuckData
{
    public int _ID;
    public GameObject _DuckObject;
}
