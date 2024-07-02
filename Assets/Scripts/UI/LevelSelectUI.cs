using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _Levels;
    [SerializeField, Tooltip("The render cams for each level in order")] private GameObject[] _RenderCams;

    public int _currentIndex;

    public void ShowNextLevel()
    {

        _RenderCams[_currentIndex].SetActive(false);
        _Levels[_currentIndex].SetActive(false);
        _currentIndex += 1;
        IsIndexOutOfBounds();
        _Levels[_currentIndex].SetActive(true);
        _RenderCams[_currentIndex].SetActive(true);
    }

    public void ShowPreviousLevel()
    {
        _RenderCams[_currentIndex].SetActive(false);
        _Levels[_currentIndex].SetActive(false);
        _currentIndex -= 1;
        IsIndexOutOfBounds();
        _Levels[_currentIndex].SetActive(true);
        _RenderCams[_currentIndex].SetActive(true);
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
