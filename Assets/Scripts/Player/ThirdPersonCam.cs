using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : InputHandler
{
    public static ThirdPersonCam instance;

    [SerializeField] private Transform orientation;
    [SerializeField] private Transform player;
    [SerializeField] private Transform camT;

    [SerializeField] private ObstaclePush pushPull;

    [SerializeField] private float rotationSpeed;

    [HideInInspector]
    public Vector3 viewDir;

    private Vector2 rotateDirection;
    private Vector2 walkDirection;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        viewDir = player.position - new Vector3(camT.position.x, player.position.y, camT.position.z);
        orientation.forward = viewDir.normalized;

        walkDirection = _Move.ReadValue<Vector2>();

        Vector3 inputDir = orientation.forward * walkDirection.y + orientation.right * walkDirection.x;

        Vector3 camForward = Vector3.Scale(camT.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 walkDir = walkDirection.y * camForward + walkDirection.x * camT.right;

        //Rotate character towards walk direction
        if (inputDir != Vector3.zero /*&& pushPull.grabbedObject == null*/) //pushPull.grabbedObject is for making player not rotate when holding object. Needs rework cause its from older script
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
