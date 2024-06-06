using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _Levels;

    private int _currentIndex;

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ShowNextLevel()
    {
        _Levels[_currentIndex].SetActive(false);
        _currentIndex += 1;
        IsIndexOutOfBounds();
        _Levels[_currentIndex].SetActive(true);
    }

    public void ShowPreviousLevel()
    {
        _Levels[_currentIndex].SetActive(false);
        _currentIndex -= 1;
        IsIndexOutOfBounds();
        _Levels[_currentIndex].SetActive(true);
    }

    public void IsIndexOutOfBounds()
    {
        if(_currentIndex >= _Levels.Length)
        {
            _currentIndex = 0;
        }
        if(_currentIndex < 0)
        {
            _currentIndex = _Levels.Length - 1;
        }
    }
}