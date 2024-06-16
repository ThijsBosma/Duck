using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : InputHandler
{
    [SerializeField] private LevelTransition _levelLoader;

    [SerializeField] private string _LevelName;

    // Update is called once per frame
    void Update()
    {
        if (_Interact.WasPressedThisFrame())
        {
            _levelLoader.GoToLevel(_LevelName);
        }
    }
}
