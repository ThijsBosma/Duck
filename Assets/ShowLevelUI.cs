using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLevelUI : FindInputBinding
{
    [SerializeField] private GameObject _LevelUI;

    [SerializeField] private SpriteRenderer _LevelImage;
    [SerializeField] private Sprite _LevelNotFoundSprite;
    [SerializeField] private Sprite _LevelUnlockedSprite;

    private LevelData _levelData;

    [HideInInspector]
    public bool _isLevelUnlocked;

    private void Start()
    {
        _levelData = GetComponent<LevelData>();

        if (!PlayerData._Instance._CompletedLevels.Contains(_levelData._LevelCompleted) && !_levelData._IsFirstLevel)
        {
            _LevelImage.sprite = _LevelNotFoundSprite;
        }
        else if (_levelData._IsFirstLevel || PlayerData._Instance._CompletedLevels.Contains(_levelData._LevelCompleted))
        {
            _LevelImage.sprite = _LevelUnlockedSprite;
            _isLevelUnlocked = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_isLevelUnlocked)
            {
                _Interact.Enable();
                SetText("to play", true, "Interact");
            }
            _LevelUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _LevelUI.SetActive(false);
            _Interact.Disable();
            InteractText.instance.ResetText();
        }
    }
}
