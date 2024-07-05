using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevel : MonoBehaviour
{
    public GameObject[] _Levels;
    public GameObject[] _RenderTextures;

    private List<WhichDucksCollected> _LevelScripts = new List<WhichDucksCollected>();

    public UIManager _UIManager;

    public int _currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _Levels.Length; i++)
        {
            _LevelScripts.Add(_Levels[i].GetComponent<WhichDucksCollected>());
        }

        for (int i = 0; i < _Levels.Length; i++)
        {
            if (PlayerData._Instance._CompletedLevels.Contains(_Levels[i].name) && i + 1 <= _Levels.Length && i - 1 >= 0)
            {
                _Levels[i - 1].SetActive(false);
                _RenderTextures[i - 1].SetActive(false);

                _LevelScripts[i + 1]._PlayButton.SetActive(true);

                _Levels[i].SetActive(true);
                _RenderTextures[i].SetActive(true);
                _currentIndex = _LevelScripts[i]._LevelIndex;

                _LevelScripts[i]._LevelImages[0].SetActive(true);
                _LevelScripts[i]._LevelImages[1].SetActive(false);

                _LevelScripts[i]._LevelTitles[0].SetActive(true);
                _LevelScripts[i]._LevelTitles[1].SetActive(false);

                _UIManager.SetFirstSelected(_LevelScripts[i]._PlayButton);
            }
            
            if(!PlayerData._Instance._CompletedLevels.Contains(_Levels[i].name) && i + 1 < _Levels.Length)
            {
                _LevelScripts[i + 1]._PlayButton.SetActive(false);
                Debug.Log("play disabled");
            }
        }
    }

    public void ShowNextLevel()
    {
        _currentIndex++;

        if (_currentIndex > _Levels.Length - 1)
        {
            _currentIndex = _Levels.Length - 1;
        }

        if (PlayerData._Instance._DuckIDs.Contains(_LevelScripts[_currentIndex - 1]._DucksToCollect[0]._ID))
        {
            _LevelScripts[_currentIndex]._LevelImages[0].SetActive(true);
            _LevelScripts[_currentIndex]._LevelImages[1].SetActive(false);

            _LevelScripts[_currentIndex]._LevelTitles[0].SetActive(true);
            _LevelScripts[_currentIndex]._LevelTitles[1].SetActive(false);
        }
        else
        {
            _LevelScripts[_currentIndex]._LevelImages[0].SetActive(false);
            _LevelScripts[_currentIndex]._LevelImages[1].SetActive(true);

            _LevelScripts[_currentIndex]._LevelTitles[0].SetActive(false);
            _LevelScripts[_currentIndex]._LevelTitles[1].SetActive(true);
        }

        _Levels[_currentIndex].SetActive(true);
        _RenderTextures[_currentIndex].SetActive(true);

        DisablePreviousLevel();
    }

    public void ShowPreviousLevel()
    {
        _currentIndex--;

        _Levels[_currentIndex].SetActive(true);
        _RenderTextures[_currentIndex].SetActive(true);

        DisableNextLevel();
    }

    private void DisablePreviousLevel()
    {
        if (!_LevelScripts[_currentIndex]._IsFirstLevel)
        {
            _RenderTextures[_currentIndex - 1].SetActive(false);
            _Levels[_currentIndex - 1].gameObject.SetActive(false);
        }
    }

    private void DisableNextLevel()
    {
        _RenderTextures[_currentIndex + 1].SetActive(false);
        _Levels[_currentIndex + 1].gameObject.SetActive(false);
    }

    public void IsIndexOutOfBounds()
    {
        if (_currentIndex >= _Levels.Length)
        {
            _currentIndex = 0;
        }
        if (_currentIndex < 0)
        {
            _currentIndex = _Levels.Length - 1;
        }
    }
}
