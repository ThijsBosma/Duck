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

    //Job please dont look
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _playerWin;

    [SerializeField] private float _transitionDelay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerData._Instance._TotalDucksCollected += PlayerData._Instance._DucksCollectedInStage;

            StartCoroutine(PlayLevelClearSequence(_transitionDelay));
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }

    private IEnumerator PlayLevelClearSequence(float delay)
    {
        if (!AudioManager._Instance.SoundIsPlaying("WinSFX"))
            AudioManager._Instance.Play("WinSFX");

        yield return new WaitForSeconds(1f);
        AudioManager._Instance.Play("LevelWin");
        _virtualCam.SetActive(true);

        _player.SetActive(false);
        _playerWin.SetActive(true);
        _Transition.GoToLevelSelection(delay);
        Destroy(gameObject);
    }
}
