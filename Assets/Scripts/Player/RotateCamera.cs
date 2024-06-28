using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateCamera : InputHandler
{
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
    private Quaternion _startRotation;

    [Header("MouseRotation")]
    private Vector3 _OldMousePosition;
    private Vector3 _MousePosition;
    

    private void Start()
    {
        _startRotation = transform.rotation;

        yRotation = _startRotation.eulerAngles.x;
        xRotation = _startRotation.eulerAngles.y;

        if (SceneManager.GetActiveScene().buildIndex == 2)
            this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager._Instance._enableCameraRotate)
            return;

        if (_Look.IsPressed())
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

        }
        transform.rotation = Quaternion.Euler(yRotation, xRotation, transform.rotation.eulerAngles.z);
        orientation.rotation = Quaternion.Euler(0, xRotation, 0);
    }

    /// <summary>
    /// Enable inputs after the camera has rotated
    /// </summary>
    public void ActivateMovement()
    {
        GameManager._Instance._enableMove = true;
    }

    public void ActivateCameraRotation()
    {
        GameManager._Instance._enableCameraRotate = true;
    }

    private void GetMousePosition()
    {

    }
}
