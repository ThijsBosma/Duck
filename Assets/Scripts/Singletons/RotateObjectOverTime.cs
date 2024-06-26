using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectOverTime : MonoBehaviour
{
    [SerializeField] private float _Speed = 2.5f;

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0, _Speed * Time.deltaTime * 10f, 0);
    }
}
