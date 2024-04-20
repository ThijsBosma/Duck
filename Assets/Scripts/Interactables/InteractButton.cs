using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    [SerializeField] protected Transform visual;
    [SerializeField] protected Transform waypoint;

    public Transform interactingObject;

    private Rigidbody interactingObjectRb;

    private PressButton button;

    private MeshFilter visualFilter;
    
    private Vector3 origin;
    private Vector3 interactingObjectStayPosition;

    private float time;

    private void Start()
    {
        button = GetComponentInParent<PressButton>();

        visualFilter = visual.GetComponent<MeshFilter>();

        origin = visual.position;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            interactingObject = other.transform;

            if (other.gameObject.CompareTag("Box"))
            {
                interactingObjectRb = other.GetComponent<Rigidbody>();
                interactingObjectRb.useGravity = false;
                interactingObjectRb.velocity = Vector3.zero;
                interactingObjectRb.freezeRotation = true;

                interactingObject.position = new Vector3(interactingObject.position.x,
                visualFilter.mesh.bounds.extents.normalized.y - visual.localPosition.y,
                interactingObject.position.z);

                interactingObjectStayPosition = interactingObject.position;
            }

            visual.position = waypoint.position;

            button.onButton = true;

            button.OnClick.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            interactingObject.position = new Vector3(interactingObject.position.x, interactingObjectStayPosition.y, interactingObject.position.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            interactingObject = null;
            button.onButton = false;

            if (other.gameObject.CompareTag("Box"))
            {
                interactingObjectRb.useGravity = true;

                interactingObjectRb.freezeRotation = false;
            }

            if (!button.holdButton && !button.onButton)
            {
                visual.position = origin;

                button.OnUnClick.Invoke();
            }
        }
    }
}
