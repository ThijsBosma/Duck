using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : FindInputBinding
{
    [Header("References")]
    [SerializeField] private Transform _Orientation;
    [SerializeField] private Image _InputIcon;

    [Header("Interact Options")]
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
    private bool _textHasReseted;

    private void Update()
    {
        if (_Interact.WasPressedThisFrame())
        {
            foreach (RaycastHit hit in colliders)
            {
                if (_Interactable == null)
                {
                    Debug.LogError($"Object {hit.collider.name} does not contain an IInteractable component");
                }
                else if (!_Interactable.HasInteracted())
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

            _Interactable.UnInteract();
        }
    }

    private void FixedUpdate()
    {
        if (!_isInteracting)
        {
            colliders = Physics.SphereCastAll(transform.position, _radius, _Orientation.forward, 0f, interactLayer);
            if (_Interactable == null && !_interactableInRange)
            {
                foreach (RaycastHit hit in colliders)
                {
                    _Interactable = hit.collider.gameObject.GetComponent<IInteractable>();
                    break;
                }
            }
        }

        bool hasColliders = colliders.Length > 0;

        if (hasColliders && !_interactableInRange)
        {
            _Interact.Enable();

            _interactableInRange = true;

            if (_Interactable.HasInteracted() ^ _interactableInRange)
            {
                if (playerInput.currentControlScheme == "PlaystationController" || playerInput.currentControlScheme == "Gamepad" || playerInput.currentControlScheme == "XboxController")
                {
                    _InputIcon.sprite = FindIconBinding();

                    Color iconColor = _InputIcon.color;
                    iconColor.a = 255;
                    _InputIcon.color = iconColor;

                    InteractText.instance.SetText($"Press       to Interact");
                }
                else if (playerInput.currentControlScheme == "Keyboard&Mouse")
                {
                    InteractText.instance.SetText($"Press {FindBinding()} to Interact");
                }
                _textHasReseted = false;
            }
        }
        else if (!hasColliders && _interactableInRange)
        {
            _Interact.Disable();
            _interactableInRange = false;

            _Interactable = null;

            if (!_textHasReseted)
            {
                InteractText.instance.ResetText();
                _textHasReseted = true;
            }
        }
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
