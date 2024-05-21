using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalAmountOfDucksShower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _TotalDuckText;

    private void Start()
    {
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        _TotalDuckText.text = "You have collected " + PlayerData._Instance._TotalDucksCollected.ToString() + " ducks";
    }
}
