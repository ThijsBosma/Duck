using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePush : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _Orientaion;
    [SerializeField] private Transform _RaycastPosition;
    [SerializeField] private Transform _HoldPosition;

    [SerializeField] private float _ForceMagnitude;

    [SerializeField] private LayerMask _BoxLayer;

    private Rigidbody rb;
    private Rigidbody grabbedObjectRb;

    private BoxCollider grabbedObjectCollider;

    private Box box;

    public GameObject grabbedObject;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RaycastHit hit;

        // grab object
        if (Input.GetKeyDown(KeyCode.F) && grabbedObject == null)
        {
            if (Physics.Raycast(_RaycastPosition.position, _Orientaion.forward, out hit, 2, _BoxLayer))
            {
                Debug.Log(hit.collider.name);

                box = hit.collider.GetComponent<Box>();

                box.grabbed = true;

                rb.mass = 1.2f;
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
        else if (Input.GetKeyUp(KeyCode.F) && grabbedObject != null)
        {
            rb.mass = 1;
            grabbedObjectCollider.isTrigger = false;

            box.grabbed = false;

            grabbedObjectRb.isKinematic = false;
            grabbedObjectRb.useGravity = true;
            grabbedObjectRb.mass = 10;

            grabbedObject.transform.SetParent(null);

            grabbedObjectCollider = null;
            grabbedObjectRb = null;
            grabbedObject = null;
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

            Interact();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            InteractText.instance.ResetText();
        }
    }

    public void Interact()
    {
        InteractText.instance.SetText("Press F to pick up");
    }
}
