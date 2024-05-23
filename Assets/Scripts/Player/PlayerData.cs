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
    public int _ObjectPicedkup;
    public int _KeyPickedup;
    public int _WateringCanPickedup;
    public int _WateringCanHasWater;
    public int _SeedPickedup;
}
