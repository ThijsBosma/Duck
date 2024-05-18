using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class ObstaclePush : FindInputBinding, IInteractable
{
    [SerializeField] private Transform _Orientaion;
    [SerializeField] private Transform _RaycastPosition;
    [SerializeField] private Transform _HoldPosition;

    [SerializeField] private LayerMask _BoxLayer;

    private Rigidbody _Rb;
    private Rigidbody grabbedObjectRb;

    private BoxCollider grabbedObjectCollider;

    private PickUpObject box = null;

    public GameObject grabbedObject;

    private bool resetText;

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
        else if (grabbedObject == null && !resetText)
        {
            InteractText.instance.ResetText();
            resetText = true;
        }


        // grab object
        if (_Interact.WasPressedThisFrame() && grabbedObject == null)
        {
            if (Physics.Raycast(_RaycastPosition.position, _Orientaion.forward, out hit, 1f, _BoxLayer))
            {
                box = hit.collider.GetComponent<PickUpObject>();

                box._grabbed = true;

                grabbedObject = hit.collider.gameObject;
                grabbedObjectCollider = grabbedObject.GetComponentInChildren<BoxCollider>();
                grabbedObjectCollider.isTrigger = true;

                grabbedObject.transform.position = _HoldPosition.position;

                grabbedObjectRb = grabbedObject.GetComponentInParent<Rigidbody>();

                grabbedObjectRb.isKinematic = true;
                grabbedObjectRb.useGravity = false;
                grabbedObjectRb.mass = 0;

                grabbedObject.transform.SetParent(_HoldPosition);
            }
        }
        // release object
        else if (_Interact.WasReleasedThisFrame() && grabbedObject != null)
        {
            _Interact.Disable();

            grabbedObjectCollider.isTrigger = false;

            box._grabbed = false;

            grabbedObjectRb.isKinematic = false;
            grabbedObjectRb.useGravity = true;
            grabbedObjectRb.mass = 10;

            grabbedObject.transform.SetParent(null);

            grabbedObjectCollider = null;
            grabbedObjectRb = null;
            grabbedObject = null;

        }

        if (_Interact.IsInProgress() && grabbedObject != null)
        {
            grabbedObject.transform.position = _HoldPosition.position;
        }
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
        resetText = false;
        InteractText.instance.SetText($"Press {FindBinding("Pickup")} to pick up");
    }

    public void UnInteract()
    {
    }

    public bool HasInteracted()
    {
        InteractText.instance.ResetText();
        return true;
    }
}
