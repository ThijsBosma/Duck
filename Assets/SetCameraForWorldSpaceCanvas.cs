using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraForWorldSpaceCanvas : MonoBehaviour
{
    private Canvas _Canvas;

    // Start is called before the first frame update
    void Start()
    {
        _Canvas = GetComponent<Canvas>();

        _Canvas.worldCamera = Camera.main;
    }
}
