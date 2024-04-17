using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public static ThirdPersonCam instance;

    [SerializeField] private Transform orientation;
    [SerializeField] private Transform player;
    [SerializeField] private Transform camT;

    [SerializeField] private float rotationSpeed;

    public Vector3 viewDir;
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        viewDir = player.position - new Vector3(camT.position.x, player.position.y, camT.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
            player.forward = Vector3.Slerp(player.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(player.position, viewDir.normalized);
    }
}
