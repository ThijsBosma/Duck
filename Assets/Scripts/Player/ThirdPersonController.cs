using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThirdPersonController : InputHandler
{
    private CharacterController controller;

    [Header("GeneralPlayerVariables")]
    [SerializeField] private float _Speed;

    [SerializeField] private float _GravityStrength;

    [SerializeField] private Transform _Orientation;
    [SerializeField] private Transform _PlayerObj;

    [Header("GroundCheck")]
    [SerializeField] private float _PlayerHeight;
    [SerializeField] private LayerMask _WhatIsGround;
    private bool _grounded;

    private Vector2 _movementInputs;

    protected Vector3 _moveDirection;

    private float _airTime;
    private float _downForce;

    private bool _inAir;

    private RaycastHit _slopeHit;

    [Header("Climbing")]
    public bool _IsClimbing;
    public Vector3 _ForwardWall;
    public Vector3 _WallUp;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Check if we are standing on a slope
        if (OnSlope())
        {
            _grounded = true;
            _inAir = false;
        }
        else
        {
            _grounded = Physics.Raycast(transform.position, Vector3.down, _PlayerHeight * 0.5f, _WhatIsGround);
        }

        Debug.DrawRay(transform.position, Vector3.down * (_PlayerHeight * 0.5f + 0.3f));

        AddDownForce();
        GetMovementInputs();
        MoveCharacter();

        if (!_grounded)
        {
            _airTime += Time.deltaTime;
            if (_airTime > 0.2f)
                _inAir = true;
        }
        else
            _inAir = false;
    }

    private void MoveCharacter()
    {
        _moveDirection = _Orientation.forward * _movementInputs.y + _Orientation.right * _movementInputs.x;

        if (!_IsClimbing)
        {
            controller.Move(new Vector3(_moveDirection.x * _Speed, _downForce, _moveDirection.z * _Speed) * Time.deltaTime);
        }
        else
        {
            //controller.Move(new Vector3(0, _movementInputs.y * _Speed / 2, _movementInputs.x * _Speed / 2) * Time.deltaTime);
            controller.Move((_ForwardWall * _movementInputs.x + _WallUp * _movementInputs.y) * _Speed / 2 * Time.deltaTime);
        }

        if (!_inAir)
        {
        }
        else if (_inAir)
        {
            //controller.Move(new Vector3(0, _downForce, 0) * Time.deltaTime);
        }
    }

    private void AddDownForce()
    {
        if (!_grounded && !_IsClimbing)
        {
            _downForce += Physics.gravity.y * _GravityStrength * Time.deltaTime;
        }
        else
        {
            _downForce = 0;
        }
    }

    private void GetMovementInputs()
    {
        _movementInputs = _Move.ReadValue<Vector2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IPlayerData>() != null)
        {
            other.gameObject.GetComponent<IPlayerData>().CollectDuck(PlayerData._Instance);
            Destroy(other.gameObject);
        }
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _PlayerHeight * 0.5f + 0.3f, _WhatIsGround))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            Debug.Log(angle < controller.slopeLimit && angle != 0);
            return angle < controller.slopeLimit && angle != 0;
        }

        return false;
    }

    private void OnDestroy()
    {
        SavePlayerDataToJSON();
    }

    private void SavePlayerDataToJSON()
    {
        string playerdata = JsonUtility.ToJson(PlayerData._Instance, true);
        string filePath = Application.persistentDataPath + "/PlayerData.json";

        Debug.Log($"Saved at {filePath}");

        System.IO.File.WriteAllText(filePath, playerdata);
        Debug.Log("PlayerData");
    }
}
