using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private Animator _Animator;

    [SerializeField] private PlayableDirector _Director;

    [SerializeField] private GameObject _Transition;

    [SerializeField] private float _transitionTime;
    [SerializeField] private float _cameraTransitionDelay;

    [SerializeField] private bool isLevelScene;

    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponentInChildren<Animator>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(DisableStartAnimationOnMainMenu());
        }

        if (isLevelScene)
        {
            _Animator.SetTrigger("StartLevel");
            StartCoroutine(DelayCameraTransition(_cameraTransitionDelay));
        }
        else
            _Animator.SetTrigger("Start");

        //GoToLevelSelection();
    }

    public void PlayDirector()
    {
        _Director.Play();
    }

    public void GoToLevelSelection(float delay)
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadScene("LevelSelection", "End", delay));
    }

    public void GoToLevel(string levelName)
    {
        StartCoroutine(LoadScene(levelName, "End", _transitionTime));
    }

    private IEnumerator DelayCameraTransition(float delay)
    {
        yield return new WaitForSeconds(delay);

        PlayDirector();
    }

    private IEnumerator LoadScene(string sceneName, string animationTriggerName, float delay)
    {
        if (animationTriggerName == "Start")
            _Animator.SetTrigger(animationTriggerName);
        else if (animationTriggerName == "End")
        {
            yield return new WaitForSeconds(delay);
            _Animator.SetTrigger(animationTriggerName);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(sceneName);
            yield return false;
        }

        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator DisableStartAnimationOnMainMenu()
    {
        _Transition.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        _Transition.SetActive(true);
    }
}
