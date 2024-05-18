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
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask interactLayer;

    private RaycastHit[] colliders;

    private IInteractable _Interactable;

    private string _interactableName;

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

            _interactableName = "";

            _Interactable.UnInteract();
            _Interactable = null;
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
                    if (_Interactable.HasInteracted())
                    {
                        _Interactable = colliders[0].collider.gameObject.GetComponent<IInteractable>();
                        _interactableName = colliders[0].collider.name;
                    }
                    else
                        _interactableName = hit.collider.name;
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
                if (_interactableName == "GrowPlant" && ((PlayerData._Instance._WateringCanPickedup == 0 && PlayerData._Instance._WateringCanHasWater == 0) ||
                        (PlayerData._Instance._WateringCanPickedup == 1 && PlayerData._Instance._WateringCanHasWater == 0) ||
                        (PlayerData._Instance._WateringCanPickedup == 0 && PlayerData._Instance._WateringCanHasWater == 1)))
                {
                    InteractText.instance.SetText("Needs water");
                }
                else if (_interactableName == "WaterPlace" && PlayerData._Instance._WateringCanPickedup == 0)
                {
                    InteractText.instance.SetText("Player needs to hold a watering can");
                }
                else if (playerInput.currentControlScheme == "PlaystationController" || playerInput.currentControlScheme == "Gamepad" || playerInput.currentControlScheme == "XboxController")
                {
                    _InputIcon.sprite = FindIconBinding();

                    Color iconColor = _InputIcon.color;
                    iconColor.a = 255;
                    _InputIcon.color = iconColor;

                    InteractText.instance.SetText($"Press       to Interact");
                }
                else if (playerInput.currentControlScheme == "Keyboard&Mouse")
                {
                    InteractText.instance.SetText($"Press {FindBinding("Interact")} to Interact");
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
}
