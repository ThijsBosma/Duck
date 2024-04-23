using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonController : InputHandler
{
    [Header("GeneralPlayerVariables")]
    private PlayerData _playerData = new PlayerData();
    [SerializeField] private Rigidbody _RigidBody;
    [SerializeField] private float _Speed;

    [SerializeField] private Transform _Orientation;
    [SerializeField] private Transform _PlayerObj;

    private Vector2 _movementInputs;

    protected Vector3 _moveDirection;

    [Header("RaycastVariables")]
    [SerializeField] private TextMeshProUGUI _InteractableText;
    [SerializeField] private float _MaxRaycastLength;

    private bool _raycastHasHit;
    private bool setText;

    private RaycastHit _hit;

    void Update()
    {
        GetMovementInputs();
        SpeedControl();
        ShootRayCast();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        _moveDirection = _Orientation.forward * _movementInputs.y + _Orientation.right * _movementInputs.x;

        _RigidBody.AddForce(_moveDirection * _Speed * 10f, ForceMode.Force);
    }

    private void GetMovementInputs()
    {
        _movementInputs = _Move.ReadValue<Vector2>();
    }

    private void SpeedControl()
    {
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
