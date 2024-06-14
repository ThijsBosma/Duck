using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private LevelTransition _Transition;

    private void OnTriggerEnter(Collider other)
    {
        PlayerData._Instance._TotalDucksCollected += PlayerData._Instance._DucksCollectedInStage;
        _Transition.GoToLevelSelection();
    }

}
