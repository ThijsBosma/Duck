using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveUp : MonoBehaviour
{
    private ThirdPersonController _controller;

    private void Start()
    {
        _controller = FindObjectOfType<ThirdPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _controller.transform.position = transform.position + new Vector3(0, 1, 0);
        }
    }
}
