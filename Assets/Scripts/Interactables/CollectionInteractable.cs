using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionInteractable : MonoBehaviour, IPlayerData
{
    public DuckData _duckData;
    public ParticleSystem _ParticleSystem;
    
    public void CollectDuck(PlayerData playerData)
    {

        if (!playerData._DuckIDs.Contains(_duckData._ID))
        {
            _ParticleSystem.Play();
            playerData._DucksCollectedInStage += 1;

            PlayerData._Instance._DuckIDs.Add(_duckData._ID);
            AudioManager._Instance.Play("Collect_item");
        }
    }
}

[System.Serializable]
public class DuckData
{
    public int _ID;
    public GameObject _DuckObject;
    public float _DuckSize;
}
