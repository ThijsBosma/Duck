using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichDucksCollected : MonoBehaviour
{
    [SerializeField, Tooltip("The ID that corresponds to the level")] private int _ImageID;

    private GameObject _ImageObject;

    private void Start()
    {
        _ImageObject = gameObject;

        if (PlayerData._Instance._DuckIDs.Contains(_ImageID))
        {
            _ImageObject.SetActive(true);
            Debug.Log("ID Found");
        }
        else
        {
            _ImageObject.SetActive(false);
            Debug.Log("No ID found");
        }
    }

}
