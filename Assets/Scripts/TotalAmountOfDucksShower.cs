using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalAmountOfDucksShower : MonoBehaviour
{
    private PlayerData _playerData;
    [SerializeField] private TextMeshProUGUI _TotalDuckText;

    private void Start()
    {
        LoadJSONFile();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        _TotalDuckText.text = "You have collected " + _playerData._DucksCollectedInStage.ToString() + " ducks";
    }

    private void LoadJSONFile()
    {
        string filePath = Application.persistentDataPath + "/PlayerData.json";

        string playerData = System.IO.File.ReadAllText(filePath);

        _playerData = JsonUtility.FromJson<PlayerData>(playerData);
        Debug.Log("Loaded files");
    }
}
