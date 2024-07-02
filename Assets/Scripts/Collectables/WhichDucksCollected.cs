using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichDucksCollected : MonoBehaviour
{
    [SerializeField] private Transform[] _UIDucks;
    [SerializeField] private DuckData[] _DucksToCollect;
    [SerializeField] private string _LevelName;
    [SerializeField] private LevelSelectUI _LevelSelectUI;

    [SerializeField] private int _LevelIndex;

    [SerializeField] private GameObject _PlayButton;
    [SerializeField] private GameObject _NextButton;

    [SerializeField] private GameObject[] _LevelImages;
    [SerializeField] private GameObject[] _LevelNames;

    [SerializeField] private bool _IsFirstLevel;


    private void Start()
    {
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
                UIDuck.layer = 10;
                if(UIDuck.GetComponent<RotateObjectOverTime>() == null && UIDuck.GetComponentInChildren<RotateObjectOverTime>() == null)
                    UIDuck.AddComponent<RotateObjectOverTime>();

                UIDuck.transform.localScale = Vector3.one * duck._DuckSize;

                Debug.Log("Duck found in array");
                Destroy(_UIDucks[timesThroughArray].gameObject);
            }
            timesThroughArray++;
        }
    }

    private void OnEnable()
    {
        if (PlayerData._Instance._CompletedLevels.Contains(_LevelName) || _IsFirstLevel)
        {
            _PlayButton.SetActive(true);
            _NextButton.SetActive(true);

            _LevelImages[0].SetActive(true);
            _LevelImages[1].SetActive(false);

            _LevelNames[0].SetActive(true);
            _LevelNames[1].SetActive(false);

            _LevelSelectUI._currentIndex = _LevelIndex;
        }
        else
        {
            _LevelImages[0].SetActive(false);
            _LevelImages[1].SetActive(true);

            _LevelNames[0].SetActive(false);
            _LevelNames[1].SetActive(true);

            _PlayButton.SetActive(false);
            _NextButton.SetActive(false);
        }
    }
}


