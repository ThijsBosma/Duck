using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichDucksCollected : MonoBehaviour
{
    [SerializeField, Tooltip("The ID that corresponds to the duck id in the level")] private int _ImageID;

    [SerializeField] private GameObject[] _DuckObjects;

    [SerializeField] private Transform[] _UIDucks;
    [SerializeField] private DuckData[] _DucksToCollect;

    private GameObject _ImageObject;
    private bool _isActivated;

    private void Start()
    {
        UpdateDucksCollected();

        //_ImageObject = gameObject;

        //if (PlayerData._Instance._DuckIDs.Contains(_ImageID))
        //{
        //    //_ImageObject.SetActive(true);
        //    //_isActivated = true;

        //    _DuckObjects[0].SetActive(false);
        //    _DuckObjects[1].SetActive(true);

        //    Debug.Log("ID Found");
        //}
        //else
        //{
        //    _isActivated = false;
        //    _DuckObjects[0].SetActive(false);
        //    _DuckObjects[1].SetActive(true);
        //    Debug.Log("No ID found");
        //}
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

                UIDuck.transform.localScale = Vector3.one * duck._DuckSize;

                Debug.Log("Duck found in array");
                Destroy(_UIDucks[timesThroughArray].gameObject);
            }
            timesThroughArray++;
        }
    }

}
