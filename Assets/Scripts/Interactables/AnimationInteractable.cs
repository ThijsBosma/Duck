using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator _InteractAnimation;
    [SerializeField] private GameObject _DuckPrefab;
    [SerializeField] private Transform _DuckSpawnPoint;
    
    public void Interact()
    {
        _InteractAnimation.SetBool("IsPressed", true);
    }

    public void SpawnObject()
    {
        Instantiate(_DuckPrefab, _DuckSpawnPoint.position, _DuckSpawnPoint.rotation);
    }
}
