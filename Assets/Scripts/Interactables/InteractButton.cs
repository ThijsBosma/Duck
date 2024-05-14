using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    [SerializeField] protected Transform visual;
    [SerializeField] protected Transform waypoint;

    public List<Transform> interactingObjects = new List<Transform>();

    private Rigidbody interactingObjectRb;

    private PressButton button;

    private MeshFilter visualFilter;

    private GrabBox box;

    private Vector3 origin;

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
        if (other.gameObject.CompareTag("Player"))
        {
            interactingObjects.Add(other.transform);

            if (!button.onButton)
            {
                visual.position = waypoint.position;

                button.onButton = true;

                button.OnClick.Invoke();
            }
        }

        if (other.gameObject.CompareTag("Box"))
        {
            interactingObjects.Add(other.transform);

            box = other.GetComponent<GrabBox>();

            interactingObjectRb = other.GetComponent<Rigidbody>();
            interactingObjectRb.useGravity = false;
            interactingObjectRb.velocity = Vector3.zero;
            interactingObjectRb.freezeRotation = true;

            visual.position = waypoint.position;

            button.onButton = true;

            button.OnClick.Invoke(); ;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            button.onButton = true;

            if (!box._grabbed && button.onButton)
            {
                interactingObjectRb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (interactingObjects.Count == 1)
            {
                button.onButton = false;
            }

            interactingObjects.Remove(other.transform);

            if (!button.holdButton && !button.onButton)
            {
                visual.position = origin;

                button.OnUnClick.Invoke();
            }
        }

        if (other.gameObject.CompareTag("Box"))
        {
            interactingObjects.Remove(other.transform);
            button.onButton = false;

            interactingObjectRb.useGravity = true;

            interactingObjectRb.constraints = RigidbodyConstraints.None;

            if (!button.holdButton && !button.onButton)
            {
                visual.position = origin;

                button.OnUnClick.Invoke();
            }
        }
    }
}
