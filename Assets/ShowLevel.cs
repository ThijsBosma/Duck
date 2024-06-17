using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevel : MonoBehaviour
{
    public LevelData[] _Levels;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _Levels.Length; i++)
        {
            if (PlayerData._Instance._CompletedLevels.Contains(_Levels[i]._LevelCompleted._levelName))
            {
                _Levels[i + 1].EnableLevel();
            }
        }
    }
}
