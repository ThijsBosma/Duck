using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public static ThirdPersonCam instance;

    [SerializeField] private Transform orientation;
    [SerializeField] private Transform player;
    [SerializeField] private Transform camT;

    [SerializeField] private ObstaclePush pushPull;

    [SerializeField] private float rotationSpeed;

    [HideInInspector]
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

        Vector3 camForward = Vector3.Scale(camT.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 walkDir = verticalInput * camForward + horizontalInput * camT.right;

        if (inputDir != Vector3.zero && pushPull.grabbedObject == null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(walkDir);
            player.rotation = Quaternion.Slerp(player.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(player.position, viewDir.normalized);
    }
}
