using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public LevelCompleted _LevelCompleted;

    [SerializeField] private DuckData[] _DucksToCollect;

    [SerializeField] private Transform[] _UIDucks;

    [SerializeField] private GameObject _LockedLevel;
    [SerializeField] private GameObject _UnlockedLevel;

    public bool _IsFirstLevel;

    private void Start()
    {
        Debug.Log(PlayerData._Instance._CompletedLevels.Contains(_LevelCompleted._levelName));

        if (!_IsFirstLevel)
        {
            if (PlayerData._Instance._CompletedLevels.Contains(_LevelCompleted._levelName))
            {
                EnableLevel();
            }
            else
            {
                _LockedLevel.SetActive(true);
                _UnlockedLevel.SetActive(false);
                return;
            }
        }

        UpdateDucksCollected();
    }

    private void UpdateDucksCollected()
    {
        int timesThroughArray = 0;
        Transform parentTransform = _UIDucks[0].parent;

        foreach (DuckData duck in _DucksToCollect)
        {
            if (PlayerData._Instance._DuckIDs.Contains(duck._ID))
            {
                GameObject UIDuck = Instantiate(duck._DuckObject, _UIDucks[timesThroughArray].position, _UIDucks[timesThroughArray].rotation, parentTransform);

                UIDuck.transform.localScale = _UIDucks[timesThroughArray].localScale;

                Debug.Log("Duck found in array");
                Destroy(_UIDucks[timesThroughArray].gameObject);
            }
            timesThroughArray++;
        }
    }

    public void EnableLevel()
    {
        _LockedLevel.SetActive(false);
        _UnlockedLevel.SetActive(true);
    }
}
