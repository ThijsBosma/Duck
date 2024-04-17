using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private Transform focusPoint;
    [SerializeField] private Transform camPos;
    [SerializeField] private Transform orientation;

    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    private float xRotation;
    private float yRotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            xRotation -= Time.deltaTime * sensX * 10;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            xRotation += Time.deltaTime * sensX * 10;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            yRotation -= Time.deltaTime * sensY * 10;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            yRotation += Time.deltaTime * sensY * 10;
        }

        transform.rotation = Quaternion.Euler(yRotation, xRotation, transform.rotation.eulerAngles.z);
        orientation.rotation = Quaternion.Euler(0, xRotation, 0);
    }
}
