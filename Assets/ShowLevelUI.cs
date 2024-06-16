using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevelUI : FindInputBinding
{
    [SerializeField] private GameObject _LevelUI;

    private void OnTriggerEnter(Collider other)
    {
        _Interact.Enable();
        _LevelUI.SetActive(true);
        SetText("to play", true, "Interact");
    }

    private void OnTriggerExit(Collider other)
    {
        _LevelUI.SetActive(false);
        _Interact.Disable();
        InteractText.instance.ResetText();
    }
}
