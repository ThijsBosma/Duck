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

        Debug.Log(_controlScheme);

        StartCoroutine(ShowControlIcons());
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

    public void Quit()
    {
        Debug.Log("it has quit the game but it no work outside of build");
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator ShowControlIcons()
    {
        if (_controlScheme == "Keyboard&Mouse")
        {
            _ControlText[0].SetActive(true);
        }
        else
        {
            _ControlText[1].SetActive(true);
        }

        yield return new WaitForSeconds(10);

        if (_controlScheme == "Keyboard&Mouse")
        {
            _ControlText[0].SetActive(false);
        }
        else
        {
            _ControlText[1].SetActive(false);
        }

    }
}
