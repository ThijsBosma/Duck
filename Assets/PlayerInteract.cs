using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteract : InputHandler
{
    [Header("RaycastVariables")]
    [SerializeField] private TextMeshProUGUI _InteractableText;
    [SerializeField] private float _MaxRaycastLength;

    private bool _raycastHasHit;
    private bool setText;

    private RaycastHit _hit;

    private void Update()
    {
        ShootRaycast();
    }

    private void ShootRaycast()
    {
        _raycastHasHit = Physics.Raycast(transform.position, transform.forward, out _hit, _MaxRaycastLength);

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
