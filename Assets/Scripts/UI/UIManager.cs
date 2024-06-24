using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : FindInputBinding
{
    [SerializeField] private GameObject _PauseAssets;
    [SerializeField] private GameObject _GUIAssets;

    [SerializeField] private GameObject[] _ControlText;

    private string _controlScheme;

    private void Start()
    {
        _controlScheme = playerInput.currentControlScheme;
    }

    void Update()
    {
        OpenPauseMenu();
    }

    private void OpenPauseMenu()
    {
        if (_Pause.WasPressedThisFrame() && !_PauseAssets.activeInHierarchy)
        {
            Time.timeScale = 0f;

            _PauseAssets.SetActive(true);
            _GUIAssets.SetActive(false);
        }
        else if (_Pause.WasPressedThisFrame() && _PauseAssets.activeInHierarchy)
        {
            Continue();
        }
    }

    public void Continue()
    {
        Time.timeScale = 1f;

        _PauseAssets.SetActive(false);
        _GUIAssets.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
