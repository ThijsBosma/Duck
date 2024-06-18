using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressButton : MonoBehaviour
{
    public UnityEvent OnClick;
    public UnityEvent OnUnClick;

    public bool holdButton;

    [HideInInspector] public bool onButton;
}
