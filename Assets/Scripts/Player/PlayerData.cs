using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData _Instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public int _DucksCollectedInStage;
    public int _TotalDucksCollected;
    public int _ObjectPicedkUp;
    public int _KeyPickedUp;
}
