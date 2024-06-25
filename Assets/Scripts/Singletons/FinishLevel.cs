using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private LevelTransition _Transition;
    [SerializeField] private GameObject _virtualCam;
    [SerializeField] private string _ClearedLevelName;

    [SerializeField] private CinemachineBrain _Brain;

    //Job please dont look
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _playerWin;

    [SerializeField] private float _transitionDelay;

    private Coroutine _transitionRoutine;

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerData._Instance._TotalDucksCollected += PlayerData._Instance._DucksCollectedInStage;

            _Brain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;

            LevelCompleted levelCompleted = new LevelCompleted();
            levelCompleted._levelName = _ClearedLevelName;
            levelCompleted._isCompleted = true;

            PlayerData._Instance._CompletedLevels.Add(levelCompleted._levelName);

            if (_transitionRoutine == null)
                _transitionRoutine = StartCoroutine(PlayLevelClearSequence(_transitionDelay));

            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            GameManager._Instance._LevelEnded = true;
        }
    }

    private IEnumerator PlayLevelClearSequence(float delay)
    {
        Debug.Log("GAy");

        if (!AudioManager._Instance.SoundIsPlaying("Collect_item"))
            AudioManager._Instance.Play("Collect_item");

        yield return new WaitForSeconds(1f);
        AudioManager._Instance.Play("LevelWin");
        _virtualCam.SetActive(true);

        _player.SetActive(false);
        _playerWin.SetActive(true);
        _Transition.GoToLevelSelection(delay);
        //Destroy(gameObject);
    }
}
