using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;

    public bool _showInputs;
    public bool _enableMove;
    public bool _enableCameraRotate;

    private void Awake()
    {
        _Instance = this;
    }
}
