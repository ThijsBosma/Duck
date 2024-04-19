using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePush : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform raycastPosition;

    [SerializeField] private LayerMask boxLayer;

    private Rigidbody rb;
    private Rigidbody grabbedObjectRb;

    private BoxCollider grabbedObjectCollider;

    private GameObject grabbedObject;

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
            if (Physics.Raycast(raycastPosition.position, orientation.forward, out hit, 2, boxLayer))
            {
                Debug.Log(hit.collider.name);

                rb.mass = 1.2f;
                grabbedObject = hit.collider.gameObject;
                grabbedObjectCollider = grabbedObject.GetComponentsInChildren<BoxCollider>()[1];
                grabbedObjectCollider.isTrigger = true;

                grabbedObjectRb = grabbedObject.GetComponent<Rigidbody>();

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

            grabbedObjectRb.isKinematic = false;
            grabbedObjectRb.useGravity = true;
            grabbedObjectRb.mass = 10;

            grabbedObject.transform.SetParent(null);

            grabbedObjectCollider = null;
            grabbedObjectRb = null;
            grabbedObject = null;
        }

        Debug.DrawRay(raycastPosition.position, orientation.forward.normalized * 2, Color.green);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            Interact();
        }
    }

    public void Interact()
    {
        Debug.Log("Press F to grab");
    }
}
