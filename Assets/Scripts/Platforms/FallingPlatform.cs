using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallingPlatform : MonoBehaviour
{
    [Header("Gravity")]
    [SerializeField] private Rigidbody _RigidBody;
    [SerializeField] private float _GravityStrength;
    private float _gravity = 9.81f;

    [Header("Delays")]
    [SerializeField] private float _FallDelay;
    [SerializeField] private float _DestroyDelay;

    private RaycastHit _hit;
    private bool _hasHit;

    private void Start()
    {
        _RigidBody.useGravity = false;
    }

    private void FixedUpdate()
    {
        ShootRayCast();
    }

    private void ShootRayCast()
    {
        _hasHit = Physics.Raycast(transform.position, transform.up, out _hit);

        if(_hasHit && _hit.collider.GetComponent<ThirdPersonController>())
        {
            _RigidBody.useGravity = true;
            StartCoroutine(PlatformFalls());
        }
    }

    private IEnumerator PlatformFalls()
    {
        yield return new WaitForSeconds(_FallDelay);
        _RigidBody.AddForce(Vector3.down * _gravity * _GravityStrength, ForceMode.Force);
        Destroy(gameObject, _DestroyDelay);
    }
}
