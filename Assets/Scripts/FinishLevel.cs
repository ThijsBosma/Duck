using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerData._Instance._TotalDucksCollected += PlayerData._Instance._DucksCollectedInStage;
        SceneManager.LoadScene("LevelFinished");
    }

}
