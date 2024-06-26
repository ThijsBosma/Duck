using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevel : MonoBehaviour
{
    public LevelData[] _Levels;

    // Start is called before the first frame update
    void Start()
    {
        Transform playerT = GameObject.Find("Player").transform;

        for (int i = 0; i < _Levels.Length; i++)
        {
            if (PlayerData._Instance._CompletedLevels.Contains(_Levels[i]._LevelCompleted._levelName))
            {
                Debug.Log("Gay");
                if (i + 1 < _Levels.Length)
                    _Levels[i + 1].EnableLevel();
                else
                    _Levels[i].EnableLevel();

                if(i - 1 < 0)
                    playerT.position = _Levels[i]._PlayerCompletedLevelPosition.position;
                else
                {
                    Debug.Log("Player set on previous position");
                    playerT.position = _Levels[i - 1]._PlayerCompletedLevelPosition.position;
                }
            }
        }
    }
}
