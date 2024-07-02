using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevel : MonoBehaviour
{
    public GameObject[] _Levels;
    public GameObject[] _RenderTextures;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _Levels.Length; i++)
        {
            if (PlayerData._Instance._CompletedLevels.Contains(_Levels[i].name))
            {
                _Levels[i].SetActive(true);
                _RenderTextures[i].SetActive(true);

                //disable the previous levels
                if (i - 1 < i && i - 1 != -1)
                {
                    _Levels[i - 1].SetActive(false);
                    _RenderTextures[i - 1].SetActive(false);
                }
                else
                {
                    _Levels[i].SetActive(false);
                    _RenderTextures[i].SetActive(false);
                }
            }
        }
    }
}
