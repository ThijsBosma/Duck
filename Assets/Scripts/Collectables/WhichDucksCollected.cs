using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichDucksCollected : MonoBehaviour
{
    [SerializeField] private Transform[] _UIDucks;
    [SerializeField] private DuckData[] _DucksToCollect;


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
                UIDuck.AddComponent<RotateObjectOverTime>();

                UIDuck.transform.localScale = Vector3.one * duck._DuckSize;

                Debug.Log("Duck found in array");
                Destroy(_UIDucks[timesThroughArray].gameObject);
            }
            timesThroughArray++;
        }
    }

}
