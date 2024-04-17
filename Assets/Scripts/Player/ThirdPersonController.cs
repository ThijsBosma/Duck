using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] private Rigidbody _RigidBody;
    [SerializeField] private float _Speed;

    private Vector3 _MovementInputs;

    [Header("RaycastVariables")]
    [SerializeField] private float _Offset;
    [SerializeField] private TextMeshProUGUI _InteractableText;

    private bool _raycastHasHit;
    private RaycastHit _hit;

    void Update()
    {
        GetMovementInputs();
        ShootRayCast();
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

    private void ShootRayCast()
    {
        _raycastHasHit = Physics.Raycast(transform.position, transform.forward + new Vector3(0, 0, _Offset), out _hit);

        if (_raycastHasHit && _hit.collider.GetComponent<IInteractable>() != null)
        {
            _InteractableText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                _hit.collider.GetComponent<IInteractable>().Interact();
            }
        }
        else
        {
            _InteractableText.gameObject.SetActive(false);
        }
    }

    private void OnValidate()
    {
        _RigidBody = GetComponent<Rigidbody>();
    }
}
