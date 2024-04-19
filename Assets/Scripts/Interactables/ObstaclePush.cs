using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePush : ThirdPersonController, IPushPull, IInteractable
{
    [SerializeField] private float forceMagnitude;

    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {

        }
    }

    public void Pull()
    {
        throw new System.NotImplementedException();
    }

    public void Push()
    {
        throw new System.NotImplementedException();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;

        if(rb != null)
        {
            Vector3 forceDirection = collision.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rb.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}
