using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    [SerializeField] protected Transform visual;
    [SerializeField] protected Transform waypoint;

    public Transform playerT;

    private Vector3 origin;

    private PressButton button;

    private void Start()
    {
        button = GetComponentInParent<PressButton>();

        origin = visual.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            playerT = other.transform;
            button.onButton = true;

            playerT.position = new Vector3(playerT.position.x,
            visual.localPosition.y + visual.localScale.y + playerT.position.y,
            playerT.position.z);

            visual.position = waypoint.position;

            button.OnClick.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            playerT = null;
            button.onButton = false;

            if (!button.holdButton)
            {
                visual.position = origin;

                button.OnUnClick.Invoke();
            }
        }
    }
}
