using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInteractable : MonoBehaviour, IPlayerData
{
    [SerializeField] private Animator _InteractAnimation;
    [SerializeField] private GameObject _DuckPrefab;
    [SerializeField] private Transform _DuckSpawnPoint;
    
    public void CollectDuck(PlayerData playerData)
    {
        _InteractAnimation.SetBool("IsPressed", true);
    }

    public void SpawnDuck()
    {
        Instantiate(_DuckPrefab, _DuckSpawnPoint.position, _DuckSpawnPoint.rotation);
    }
}
