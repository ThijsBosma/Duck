using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : InputHandler
{
    [SerializeField] private Transform focusPoint;
    [SerializeField] private Transform camPos;
    [SerializeField] private Transform orientation;

    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] private float minYRotation;
    [SerializeField] private float maxYRotation;

    [SerializeField] private bool invertCameraAxis;

    private float xRotation;
    private float yRotation;

    private Vector2 lookDir;
    private Vector3 rotateDir;

    private void Start()
    {
        camPos.LookAt(focusPoint);
    }

    // Update is called once per frame
    void Update()
    {
        lookDir = _Look.ReadValue<Vector2>();

        if (lookDir.x > 0.5f || lookDir.x < -0.5f)
        {
            if (invertCameraAxis)
                xRotation += Time.deltaTime * sensX * 10 * (lookDir.x * -1);
            else
                xRotation += Time.deltaTime * sensX * 10 * lookDir.x;
        }
        else if (lookDir.y > 0.5f || lookDir.y < -0.5f)
        {
            yRotation += Time.deltaTime * sensY * 10 * lookDir.y;
            yRotation = Mathf.Clamp(yRotation, minYRotation, maxYRotation);
        }

        transform.rotation = Quaternion.Euler(yRotation, xRotation, transform.rotation.eulerAngles.z);
        orientation.rotation = Quaternion.Euler(0, xRotation, 0);
    }
}
