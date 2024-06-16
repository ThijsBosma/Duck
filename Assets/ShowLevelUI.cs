using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevelUI : InputHandler
{
    [SerializeField] private GameObject _LevelUI;

    private void OnTriggerEnter(Collider other)
    {
        _Interact.Enable();
        _LevelUI.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        _LevelUI.SetActive(false);
        _Interact.Disable();
    }
}
