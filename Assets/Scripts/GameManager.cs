using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance { get; private set; }

    public bool _showInputs;
    public bool _enableMove;
    public bool _enableCameraRotate;

    private void Awake()
    {
        if(_Instance != null && _Instance != this)
        {
            Destroy(this);
        }
        else
        {
            _Instance = this;
        }
    }
}
