using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _PauseAssets;
    [SerializeField] private GameObject _GUIAssets;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_PauseAssets.activeInHierarchy)
        {
            Time.timeScale = 0f;

            _PauseAssets.SetActive(true);
            _GUIAssets.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && _PauseAssets.activeInHierarchy)
        {
            Time.timeScale = 1f;

            _PauseAssets.SetActive(false);
            _GUIAssets.SetActive(true);
        }
    }

    public void Continue()
    {
        Time.timeScale = 1f;

        _PauseAssets.SetActive(false);
        _GUIAssets.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("it has quit the game but it no work outside of build");
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
