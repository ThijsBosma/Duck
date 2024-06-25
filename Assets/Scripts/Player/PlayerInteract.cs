using System;
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
    [SerializeField] private Vector3 _OffsetPosition;

    [Header("Interact Options")]
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private FillWaterUI _WaterUI;

    private RaycastHit[] colliders;

    private IInteractable _Interactable;

    private IInteractable _growTreeInteractable;
    private IInteractable _pushTreeInteractable;
    private IInteractable _leverInteractable;
    private IInteractable _waterPlaceInteractable;

    private string _interactableName;

    private bool _interactableInRange;
    private bool _isInteracting;
    private bool _textHasReseted;

    private void Update()
    {
        if (_Interact.WasPressedThisFrame())
        {
            if (_growTreeInteractable != null && !_growTreeInteractable.HasInteracted())
            {
                _growTreeInteractable.Interact();
                _WaterUI.FillOutUI();
            }
            else if (_pushTreeInteractable != null && !_pushTreeInteractable.HasInteracted())
                _pushTreeInteractable.Interact();
            else if (_waterPlaceInteractable != null && !_waterPlaceInteractable.HasInteracted())
                _waterPlaceInteractable.Interact();
            else if (_leverInteractable != null && !_leverInteractable.HasInteracted())
                _leverInteractable.Interact();
        }

        bool hasColliders = colliders.Length > 0;

        if (hasColliders)
        {
            HandleInteraction();
        }
        else if (!_textHasReseted)
        {
            ResetInteraction();
        }
    }

    private void FixedUpdate()
    {
        if (!_isInteracting)
        {
            colliders = Physics.SphereCastAll(transform.position + _OffsetPosition, _radius, _Orientation.forward, 0f, interactLayer);
            foreach (RaycastHit hit in colliders)
            {
                IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();

                if (interactable != null && !interactable.HasInteracted())
                {
                    if (interactable is GrowPlant)
                        _growTreeInteractable = interactable;
                    else if (interactable is PushTree)
                        _pushTreeInteractable = interactable;
                    else if (interactable is GetWater)
                        _waterPlaceInteractable = interactable;
                    else if (interactable is Lever)
                        _leverInteractable = interactable;

                    _interactableName = hit.collider.name;
                    _textHasReseted = false;
                    break;
                }
            }
        }
    }

    private void HandleInteraction()
    {
        if (_growTreeInteractable != null && !_growTreeInteractable.HasInteracted())
            HandleGrowTreeInteraction();
        else if (_pushTreeInteractable != null && !_pushTreeInteractable.HasInteracted())
            HandlePushTreeInteraction();
        else if (_waterPlaceInteractable != null && !_waterPlaceInteractable.HasInteracted())
            HandleWaterplaceInteraction();
        else if (_leverInteractable != null && !_leverInteractable.HasInteracted())
            HandleLeverInteraction();
    }

    private void HandleGrowTreeInteraction()
    {
        bool playerHasWateringCan = PlayerData._Instance._WateringCanPickedup == 1;
        bool wateringCanHasWater = PlayerData._Instance._WateringCanHasWater == 1;

        if ((playerHasWateringCan && !wateringCanHasWater) && !_growTreeInteractable.HasInteracted())
        {
        }
        else if (_growTreeInteractable != null && playerHasWateringCan && !_growTreeInteractable.HasInteracted())
        {
            // Enable interaction
            _Interact.Enable();
            _interactableInRange = true;
            ChangeInputIcons._Instance.UpdateUIIcons(playerInput);
        }
        else if (_growTreeInteractable.HasInteracted())
        {
            ResetInteraction();
        }
    }

    private void HandlePushTreeInteraction()
    {
        bool playerHasWateringCan = PlayerData._Instance._WateringCanPickedup == 1;

        if (playerHasWateringCan)
        {
        }
        else if (_pushTreeInteractable != null && !_pushTreeInteractable.HasInteracted())
        {
            // Enable interaction
            _Interact.Enable();
            _interactableInRange = true;

            ChangeInputIcons._Instance.UpdateUIIcons(playerInput);
        }
    }

    private void HandleWaterplaceInteraction()
    {
        bool playerHasWateringCan = PlayerData._Instance._WateringCanPickedup == 1;
        bool wateringCanHasWater = PlayerData._Instance._WateringCanHasWater == 1;

        if (!playerHasWateringCan)
        {
        }
        // Logic for Push Tree interaction text
        else if (_waterPlaceInteractable != null)
        {
            if (wateringCanHasWater)
            {
                ChangeInputIcons._Instance.UpdateUIIcons(playerInput);
            }
            else
            {
                // Enable interaction
                _Interact.Enable();
                _interactableInRange = true;
                ChangeInputIcons._Instance.UpdateUIIcons(playerInput);
            }
        }
    }

    private void HandleLeverInteraction()
    {
        if (_leverInteractable != null && !_leverInteractable.HasInteracted())
        {
            // Enable interaction
            _Interact.Enable();
            _interactableInRange = true;
        }
    }

    private void ResetInteraction()
    {
        // Disable interaction
        _Interact.Disable();
        _interactableInRange = false;

        // Reset interaction text only once
        if (!_textHasReseted)
        {
            //InteractText.instance.ResetText();
            _interactableName = "";
            _textHasReseted = true;
        }

        // Reset interactables to null
        _growTreeInteractable = null;
        _pushTreeInteractable = null;
        _waterPlaceInteractable = null;
        _leverInteractable = null;
    }

    private void OnDrawGizmos()
    {
        if (!_isInteracting)
            Gizmos.DrawWireSphere(transform.position + _OffsetPosition, _radius);
    }
}
