using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThirdPersonController : InputHandler
{
    [Header("GeneralPlayerVariables")]
    private PlayerData _playerData = new PlayerData();
    [SerializeField] private Rigidbody _RigidBody;
    [SerializeField] private float _Speed;

    [SerializeField] private float _GravityStrength;

    [SerializeField] private Transform _Orientation;
    [SerializeField] private Transform _PlayerObj;

    [Header("GroundCheck")]
    [SerializeField] private float _GroundDrag;
    [SerializeField] private float _PlayerHeight;
    [SerializeField] private LayerMask _WhatIsGround;
    private bool _grounded;

    [Header("Slope Handling")]
    [SerializeField] private float _MaxSlopeAngle;

    private RaycastHit _slopeHit;

    private Vector2 _movementInputs;

    protected Vector3 _moveDirection;

    [Header("RaycastVariables")]
    [SerializeField] private TextMeshProUGUI _InteractableText;
    [SerializeField] private float _MaxRaycastLength;

    private bool _raycastHasHit;
    private bool setText;

    public bool _onSlope;

    private RaycastHit _hit;

    private float _airTime;

    void Update()
    {
        _grounded = Physics.Raycast(transform.position, Vector3.down, _PlayerHeight * 0.5f + 0.2f, _WhatIsGround);

        _onSlope = OnSlope();

        Debug.DrawRay(transform.position, _PlayerObj.forward.normalized * (0.5f + 0.3f));

        GetMovementInputs();

        if (_grounded)
            _RigidBody.drag = _GroundDrag;
        else
            _RigidBody.drag = 0;

        SpeedControl();
        ShootRayCast();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        if (!_grounded)
        {
            _airTime += Time.deltaTime;
            if (_airTime > 0.15f && !OnSlope())
            {
                //_moveDirection = Vector3.zero;
                //_RigidBody.velocity = new Vector3(0, _RigidBody.velocity.y, 0);
            }
        }
        else
        {
            _moveDirection = _Orientation.forward * _movementInputs.y + _Orientation.right * _movementInputs.x;
            _airTime = 0;
        }

        if (OnSlope())
        {
            _RigidBody.AddForce(GetSlopeMoveDirection() * _Speed * 10f, ForceMode.Force);

            if(_RigidBody.velocity.y < 0)
            {
                _RigidBody.AddForce(Vector3.down * 10f, ForceMode.Force);
            }

            if (_movementInputs == Vector2.zero)
            {
                _RigidBody.velocity = Vector3.zero;
            }
        }

        _RigidBody.AddForce(_moveDirection * _Speed * 10f, ForceMode.Force);

        if (!_grounded && !OnSlope())
            //_RigidBody.AddForce(Vector3.down * -Physics.gravity.y * _GravityStrength, ForceMode.Force);

            if (_movementInputs != Vector2.zero)
                _RigidBody.useGravity = !OnSlope();
            else
                _RigidBody.useGravity = !_grounded;
    }

    private void GetMovementInputs()
    {
        _movementInputs = _Move.ReadValue<Vector2>();
    }

    private void SpeedControl()
    {
        if (OnSlope())
        {
            if (_RigidBody.velocity.magnitude > _Speed)
                _RigidBody.velocity = _RigidBody.velocity.normalized * _Speed;
        }

        Vector3 flatVel = new Vector3(_RigidBody.velocity.x, 0f, _RigidBody.velocity.z);

        if (flatVel.magnitude > _Speed)
        {
            Vector3 limitedVel = flatVel.normalized * _Speed;
            _RigidBody.velocity = new Vector3(limitedVel.x, _RigidBody.velocity.y, limitedVel.z);
        }
    }

    private void ShootRayCast()
    {
        _raycastHasHit = Physics.Raycast(transform.position, _PlayerObj.forward, out _hit, _MaxRaycastLength);

        if (_raycastHasHit && _hit.collider.GetComponent<AnimationInteractable>() != null)
        {
            setText = true;
            _Interact.Enable();
            InteractText.instance.SetText("Press F to interact");

            if (_Interact.IsPressed())
            {
                _hit.collider.GetComponent<IInteractable>().Interact();
            }
        }
        else if (setText)
        {
            InteractText.instance.ResetText();
            setText = false;
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down + _PlayerObj.forward, out _slopeHit, _PlayerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < _MaxSlopeAngle && angle != 0;
        }
        else if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _PlayerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < _MaxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(_moveDirection, _slopeHit.normal).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IPlayerData>() != null)
        {
            other.gameObject.GetComponent<IPlayerData>().CollectDuck(_playerData);
            Destroy(other.gameObject);
        }
    }

    private void OnValidate()
    {
        _RigidBody = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        SavePlayerDataToJSON();
    }

    private void SavePlayerDataToJSON()
    {
        string playerdata = JsonUtility.ToJson(_playerData, true);
        string filePath = Application.persistentDataPath + "/PlayerData.json";

        Debug.Log($"Saved at {filePath}");

        System.IO.File.WriteAllText(filePath, playerdata);
        Debug.Log("PlayerData");
    }
}
