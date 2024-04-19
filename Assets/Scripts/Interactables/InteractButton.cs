using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    [SerializeField] protected Transform visual;
    [SerializeField] protected Transform waypoint;

    public Transform interactingObject;

    private Vector3 origin;

    private PressButton button;

    private void Start()
    {
        button = GetComponentInParent<PressButton>();

        origin = visual.position;
    }

    private void Update()
    {
        /*if (button.onButton)
        {
            Mathf.Round(interactingObject.position.y);

            interactingObject.position = new Vector3(interactingObject.position.x,
            visual.localScale.y + Mathf.Round(interactingObject.position.y),
            interactingObject.position.z);

            button.onButton = true;
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            interactingObject = other.transform;

            if (other.gameObject.CompareTag("Box"))
            {
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

            interactingObject.position = new Vector3(interactingObject.position.x,
            visual.localScale.y + Mathf.Round(interactingObject.position.y),
            interactingObject.position.z);

            visual.position = waypoint.position;

            button.onButton = true;

            button.OnClick.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
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
                other.GetComponent<Rigidbody>().useGravity = true;
            }

            if (!button.holdButton && !button.onButton)
            {
                visual.position = origin;

                button.OnUnClick.Invoke();
            }
        }
    }
}
