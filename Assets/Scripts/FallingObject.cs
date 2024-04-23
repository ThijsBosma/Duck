using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallingObject : MonoBehaviour
{
    [SerializeField] private Rigidbody _RigidBody;

    [SerializeField] private float _TimeBeforeFall;

    private void Start()
    {
        _RigidBody.useGravity = false;
    }
    private IEnumerator ObjectFalls()
    {
        yield return new WaitForSeconds(_TimeBeforeFall);
        _RigidBody.useGravity = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<ThirdPersonController>() != null)
        {
            StartCoroutine(ObjectFalls());
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ThirdPersonController>() != null)
        {
            Destroy(gameObject);
        }
    }

    private void OnValidate()
    {
        _RigidBody = GetComponent<Rigidbody>();
    }
}
