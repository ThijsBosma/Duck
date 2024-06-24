using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : InputHandler
{
    [SerializeField] private LevelTransition _levelLoader;

    [SerializeField] private string _LevelName;

    private ShowLevelUI _levelUI;

    private void Start()
    {
        _levelUI = GetComponent<ShowLevelUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_levelUI._isLevelUnlocked)
        {
            if (_levelUI._LevelInteract.WasPressedThisFrame())
            {
                _levelLoader.GoToLevel(_LevelName);
            }
        }
    }
}
