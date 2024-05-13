using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class ObstaclePush : InputHandler, IInteractable
{
    [SerializeField] private Transform _Orientaion;
    [SerializeField] private Transform _RaycastPosition;
    [SerializeField] private Transform _HoldPosition;

    [SerializeField] private float _ForceMagnitude;

    [SerializeField] private LayerMask _BoxLayer;

    private Rigidbody _Rb;
    private Rigidbody grabbedObjectRb;

    private BoxCollider grabbedObjectCollider;

    private Box box;

    public GameObject grabbedObject;

    private void Start()
    {
        _Rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(_RaycastPosition.position, _Orientaion.forward, out hit, 1f, _BoxLayer) && grabbedObject == null)
        {
            _Interact.Enable();
            Interact();
        }

        // grab object
        if (_Interact.WasPressedThisFrame() && grabbedObject == null)
        {
            if (Physics.Raycast(_RaycastPosition.position, _Orientaion.forward, out hit, 1f, _BoxLayer))
            {
                Debug.Log(hit.collider.name);

                box = hit.collider.GetComponent<Box>();

                box.grabbed = true;

                _Rb.mass = 1.2f;
                grabbedObject = hit.collider.gameObject;
                grabbedObjectCollider = grabbedObject.GetComponentInChildren<BoxCollider>();
                grabbedObjectCollider.isTrigger = true;

                grabbedObject.transform.localPosition = _HoldPosition.position;

                grabbedObjectRb = grabbedObject.GetComponentInParent<Rigidbody>();

                grabbedObjectRb.isKinematic = true;
                grabbedObjectRb.useGravity = false;
                grabbedObjectRb.mass = 0;

                grabbedObject.transform.SetParent(transform);
            }
        }
        // release object
        else if (_Interact.WasReleasedThisFrame() && grabbedObject != null)
        {
            _Interact.Disable();

            _Rb.mass = 1;
            grabbedObjectCollider.isTrigger = false;

            box.grabbed = false;

            grabbedObjectRb.isKinematic = false;
            grabbedObjectRb.useGravity = true;
            grabbedObjectRb.mass = 10;

            grabbedObject.transform.SetParent(null);

            grabbedObjectCollider = null;
            grabbedObjectRb = null;
            grabbedObject = null;

            InteractText.instance.ResetText();
        }

        Debug.DrawRay(_RaycastPosition.position, _Orientaion.forward.normalized * 2, Color.green);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            /*Vector3 forceDirection = collision.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rb.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);*/

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            InteractText.instance.ResetText();
        }
    }

    public void Interact()
    {
        InteractText.instance.SetText($"Press {FindInputBinding()} to pick up");
    }

    private string FindInputBinding()
    {
        InputAction interactAction = playerInput.actions.FindAction("Interact");

        if (interactAction != null)
        {
            string controlScheme = playerInput.currentControlScheme;

            InputBinding? bindingForControlScheme = null;

            foreach (var binding in interactAction.bindings)
            {
                if (binding.groups.Contains(controlScheme))
                {
                    bindingForControlScheme = binding;
                    break;
                }
            }

            if (bindingForControlScheme != null)
            {
                string buttonName = ExtractButtonName(bindingForControlScheme.Value.path);
                return buttonName;
            }
            else
            {
                return "No binding for current control scheme";
            }
        }
        else
        {
            Debug.LogError("Player input not found");
            return "";
        }
    }

    private string ExtractButtonName(string bindingPath)
    {
        string[] splitPath = bindingPath.Split('/');
        if (splitPath.Length > 1)
        {

            return splitPath[splitPath.Length - 1].ToUpperInvariant();
        }
        else
        {
            return "Unknown Button";
        }
    }
}
