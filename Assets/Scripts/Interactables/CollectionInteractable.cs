using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionInteractable : MonoBehaviour, IPlayerData
{
    public DuckData _duckData;
    public ParticleSystem _ParticleSystem;
    public GameObject _Accesories;

    private MeshRenderer _DuckMesh;
    private RotateObjectOverTime _RotateObject;

    private void Start()
    {
        _DuckMesh = GetComponent<MeshRenderer>();
        _RotateObject = GetComponentInParent<RotateObjectOverTime>();
    }

    public void CollectDuck(PlayerData playerData)
    {
        if (!playerData._DuckIDs.Contains(_duckData._ID))
        {
            _DuckMesh.enabled = false;

            if (_Accesories != null)
                _Accesories.SetActive(false);

            _RotateObject._Speed = 0;

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
