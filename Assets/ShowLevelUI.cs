using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    
    [HideInInspector]
    public InputAction _LevelInteract;

    protected override void Awake()
    {
        base.Awake();
        _LevelInteract = _Input.FindAction("Interact");
    }

    private void Start()
    {
        _levelData = GetComponent<LevelData>();

        if (!PlayerData._Instance._CompletedLevels.Contains(_levelData._LevelCompleted._levelName) && !_levelData._IsFirstLevel)
        {
            _LevelImage.sprite = _LevelNotFoundSprite;
        }
        else if (_levelData._IsFirstLevel || PlayerData._Instance._CompletedLevels.Contains(_levelData._LevelCompleted._levelName))
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
                if (_LevelInteract != null)
                {
                    _LevelInteract.Enable();

                    SetText("to play", true, "Interact");
                }
            }
            _LevelUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _LevelUI.SetActive(false);
            _LevelInteract.Disable();
        }
    }
}
