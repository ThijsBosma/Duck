using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevel : MonoBehaviour
{
    public GameObject[] _Levels;
    public GameObject[] _RenderTextures;

    [SerializeField] private GameObject _NextButton;
    [SerializeField] private GameObject _PreviousButton;

    private List<WhichDucksCollected> _LevelScripts = new List<WhichDucksCollected>();

    public int _currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        _PreviousButton.SetActive(false);

        for (int i = 0; i < _Levels.Length; i++)
        {
            _LevelScripts.Add(_Levels[i].GetComponent<WhichDucksCollected>());
        }

        for (int i = 0; i < _Levels.Length; i++)
        {
            if (PlayerData._Instance._CompletedLevels.Contains(_Levels[i].name) && i + 1 <= _Levels.Length && i - 1 >= 0)
            {
                _PreviousButton.SetActive(true);

                _Levels[i - 1].SetActive(false);
                _RenderTextures[i - 1].SetActive(false);

                _Levels[i].SetActive(true);
                _RenderTextures[i].SetActive(true);
                _currentIndex = _LevelScripts[i]._LevelIndex;

                _LevelScripts[i]._LevelImages[0].SetActive(true);
                _LevelScripts[i]._LevelImages[1].SetActive(false);

                _LevelScripts[i]._LevelTitles[0].SetActive(true);
                _LevelScripts[i]._LevelTitles[1].SetActive(false);

                if (i == _Levels.Length - 1)
                {
                    _NextButton.SetActive(false);
                }
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

        _PreviousButton.SetActive(true);

        if (PlayerData._Instance._DuckIDs.Contains(_LevelScripts[_currentIndex - 1]._DucksToCollect[0]._ID))
        {
            _NextButton.SetActive(true);

            _LevelScripts[_currentIndex]._LevelImages[0].SetActive(true);
            _LevelScripts[_currentIndex]._LevelImages[1].SetActive(false);

            _LevelScripts[_currentIndex]._LevelTitles[0].SetActive(true);
            _LevelScripts[_currentIndex]._LevelTitles[1].SetActive(false);

            _LevelScripts[_currentIndex]._PlayButton.SetActive(true);
        }
        else
        {
            _NextButton.SetActive(false);

            _LevelScripts[_currentIndex]._LevelImages[0].SetActive(false);
            _LevelScripts[_currentIndex]._LevelImages[1].SetActive(true);

            _LevelScripts[_currentIndex]._LevelTitles[0].SetActive(false);
            _LevelScripts[_currentIndex]._LevelTitles[1].SetActive(true);

            if (_LevelScripts[_currentIndex]._IsFirstLevel)
                _LevelScripts[_currentIndex]._PlayButton.SetActive(true);
            else
                _LevelScripts[_currentIndex]._PlayButton.SetActive(false);
        }

        _Levels[_currentIndex].SetActive(true);
        _RenderTextures[_currentIndex].SetActive(true);

        if (_currentIndex == _Levels.Length - 1)
        {
            _NextButton.SetActive(false);
        }

        DisablePreviousLevel();
    }

    public void ShowPreviousLevel()
    {
        _currentIndex--;

        _Levels[_currentIndex].SetActive(true);
        _RenderTextures[_currentIndex].SetActive(true);

        _NextButton.SetActive(true);

        if (_currentIndex == 0)
        {
            _PreviousButton.SetActive(false);
        }

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
