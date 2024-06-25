using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichDucksCollected : MonoBehaviour
{
    [SerializeField, Tooltip("The ID that corresponds to the duck id in the level")] private int _ImageID;

    private GameObject _ImageObject;
    private bool _isActivated;

    private void Start()
    {
        _ImageObject = gameObject;

        if (PlayerData._Instance._DuckIDs.Contains(_ImageID))
        {
            _ImageObject.SetActive(true);
            _isActivated = true;
            Debug.Log("ID Found");
        }
        else
        {
            _ImageObject.SetActive(false);
            _isActivated = false;
            Debug.Log("No ID found");
        }
    }

    //private void Update()
    //{
    //    if (PlayerData._Instance._DuckIDs.Contains(_ImageID) && _isActivated == false)
    //    {
    //        _ImageObject.SetActive(true);
    //        Debug.Log("ID Found");
    //    }
    //}

}
