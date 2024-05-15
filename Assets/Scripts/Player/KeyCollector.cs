using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    public static KeyCollector Instance;

    public int _Key;

    private void Start()
    {
        Instance = this;
    }
}
