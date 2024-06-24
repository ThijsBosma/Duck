using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Transform _Cam;
    public bool _debug;

    private void Update()
    {
        transform.LookAt(transform.position + _Cam.forward);
    }

    private void OnDrawGizmos()
    {
        if(_debug)
            transform.LookAt(transform.position + _Cam.forward);
    }
}
