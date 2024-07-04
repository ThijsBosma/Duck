using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichDucksCollected : MonoBehaviour
{
    [SerializeField] private Transform[] _UIDucks;
    public DuckData[] _DucksToCollect;
    [SerializeField] private string _LevelName;
    [SerializeField] private ShowLevel _ShowLevelScript;

    public int _LevelIndex;

    public GameObject _PlayButton;

    public GameObject[] _LevelImages;
    public GameObject[] _LevelTitles;

    public bool _IsFirstLevel;

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
                if (UIDuck.GetComponent<RotateObjectOverTime>() == null && UIDuck.GetComponentInChildren<RotateObjectOverTime>() == null)
                    UIDuck.AddComponent<RotateObjectOverTime>();

                UIDuck.transform.localScale = Vector3.one * duck._DuckSize;

                Debug.Log("Duck found in array");
                Destroy(_UIDucks[timesThroughArray].gameObject);

                _ShowLevelScript._currentIndex = _LevelIndex;
            }
            timesThroughArray++;
        }
    }

    private void OnEnable()
    {

    }
}


