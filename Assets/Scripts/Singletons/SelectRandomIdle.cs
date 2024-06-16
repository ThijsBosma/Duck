using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRandomIdle : MonoBehaviour
{
    private Animator _Animator;

    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();

        _Animator.SetInteger("RandomIdle", Random.Range(0, 3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
