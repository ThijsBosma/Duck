using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _RigidBody;
    [SerializeField] private float _Speed;

    private Vector3 _MovementInputs;
    void Update()
    {
        GetMovementInputs();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        _RigidBody.AddForce(_MovementInputs * _Speed);
    }

    private void GetMovementInputs()
    {
        _MovementInputs = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void OnValidate()
    {
        _RigidBody = GetComponent<Rigidbody>();
    }
}
