using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionInteractable : MonoBehaviour, IPlayerData
{
    public void CollectDuck(PlayerData playerData)
    {
        playerData._DucksCollectedInStage += 1;
        Debug.Log(playerData._DucksCollectedInStage);
    }

    private void OnTriggerEnter(Collider other)
    {
        CollectDuck(other.GetComponent<ThirdPersonController>()._playerData);
        Destroy(gameObject);
    }
}
