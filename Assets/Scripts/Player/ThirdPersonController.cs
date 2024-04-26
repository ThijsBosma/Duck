using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThirdPersonController : InputHandler
{
    private CharacterController controller;

    [Header("GeneralPlayerVariables")]
    private PlayerData _playerData = new PlayerData();
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

    [Header("RaycastVariables")]
    [SerializeField] private TextMeshProUGUI _InteractableText;
    [SerializeField] private float _MaxRaycastLength;

    private bool _raycastHasHit;
    private bool setText;
    private bool _inAir;

    private RaycastHit _hit;

    private float _airTime;
    private float _downForce;

    void Update()
    {
        _grounded = Physics.Raycast(transform.position, Vector3.down, _PlayerHeight * 0.5f + 0.2f, _WhatIsGround);

        Debug.DrawRay(transform.position, Vector3.down * (_PlayerHeight * 0.5f + 0.2f));

        _inAir = !_grounded;

        GetMovementInputs();
        MoveCharacter();
        ShootRayCast();
        AddDownForce();

        if (!_grounded)
        {
            _airTime += Time.deltaTime;
            if (_airTime > 0.1f)
                _inAir = _grounded;
        }
    }

    private void MoveCharacter()
    {
        _moveDirection = _Orientation.forward * _movementInputs.y + _Orientation.right * _movementInputs.x;

        if (_inAir)
        {
            controller.Move(new Vector3(0, _downForce, 0) * Time.deltaTime);
        }
        else if (_grounded)
        {
            _inAir = false;
            controller.Move(new Vector3(_moveDirection.x * _Speed, _downForce, _moveDirection.z * _Speed) * Time.deltaTime);
        }
    }

    private void AddDownForce()
    {
        if (!_grounded)
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

    private void OnTriggerEnter(Collider other)
    {
        _airTime = 0;
        if (other.gameObject.GetComponent<IPlayerData>() != null)
        {
            other.gameObject.GetComponent<IPlayerData>().CollectDuck(_playerData);
            Destroy(other.gameObject);
        }
    }

    private void OnValidate()
    {
        controller = GetComponent<CharacterController>();
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
