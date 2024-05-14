using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteract : InputHandler
{
    [SerializeField] private Transform _Orientation;

    [Header("RaycastVariables")]
    [SerializeField] private TextMeshProUGUI _InteractableText;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask interactLayer;

    private RaycastHit[] colliders;

    private IInteractable _Interactable;

    private bool _raycastHasHit;
    private bool setText;

    private RaycastHit _hit;
    private bool _interactableInRange;
    private bool _isInteracting;

    private void Update()
    {
        //ShootRaycast();

        if (_Interact.IsPressed())
        {
            foreach (RaycastHit hit in colliders)
            {
                _Interactable = hit.collider.gameObject.GetComponent<IInteractable>();

                if (_Interactable == null)
                {
                    Debug.LogError($"Object {hit.collider.name} does contain an IInteractable component");
                }
                else
                {
                    if (_isInteracting == false)
                    {
                        _isInteracting = true;
                        _Interactable.Interact();
                    }

                    Debug.Log($"Interacted with {hit.collider.name}");
                }
            }
        }

        if (_Interact.WasReleasedThisFrame())
        {
            _isInteracting = false;
            _Interactable = null;

            _Interact.Disable();
        }
    }

    private void FixedUpdate()
    {
        if (!_isInteracting)
            colliders = Physics.SphereCastAll(transform.position, _radius, _Orientation.forward, 0f, interactLayer);

        if (!_interactableInRange)
        {
            if (colliders.Length > 0)
            {
                _Interact.Enable();
                _interactableInRange = true;
            }
        }

        if (colliders.Length == 0)
            _interactableInRange = false;
    }

    private void OnDrawGizmos()
    {
        if (!_isInteracting)
            Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private void ShootRaycast()
    {
        _raycastHasHit = Physics.Raycast(transform.position, transform.forward, out _hit, _radius);

        if (_raycastHasHit && _hit.collider.GetComponent<AnimationInteractable>() != null)
        {
            setText = true;
            _Interact.Enable();
            InteractText.instance.SetText("Press F to interact");

            if (_Interact.IsPressed())
            {
                _hit.collider.GetComponent<IInteractable>().Interact();
            }
        }
        else if (setText)
        {
            InteractText.instance.ResetText();
            setText = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

    }
}
