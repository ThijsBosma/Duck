using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : InputHandler
{
    [SerializeField] private GameObject _PauseAssets;
    [SerializeField] private GameObject _GUIAssets;

    [SerializeField] private GameObject[] _ControlText;

    private void Start()
    {
        StartCoroutine(DeactivateControlIcons());
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

    private IEnumerator DeactivateControlIcons()
    {
        yield return new WaitForSeconds(5f);

        for (int i = 0; i < _ControlText.Length; i++)
        {
            _ControlText[i].SetActive(false);
        }
    }
}
