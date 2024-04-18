using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonController : MonoBehaviour
{
    [Header("GeneralPlayerVariables")]
    private PlayerData _playerData = new PlayerData();
    [SerializeField] private Rigidbody _RigidBody;
    [SerializeField] private float _Speed;

    [SerializeField] private Transform _Orientation;

    private Vector3 _movementInputs;

    [Header("RaycastVariables")]
    [SerializeField] private TextMeshProUGUI _InteractableText;
    [SerializeField] private float _MaxRaycastLength;

    private bool _raycastHasHit;
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
        Vector3 moveDirection = _Orientation.forward * _movementInputs.z + _Orientation.right * _movementInputs.x;

        _RigidBody.AddForce(moveDirection * _Speed * 10f, ForceMode.Force);
    }

    private void GetMovementInputs()
    {
        _movementInputs = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
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
        _raycastHasHit = Physics.Raycast(transform.position, transform.forward, out _hit, _MaxRaycastLength);

        if (_raycastHasHit && _hit.collider.GetComponent<AnimationInteractable>() != null)
        {
            _InteractableText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                _hit.collider.GetComponent<IInteractable>().Interact(_playerData);
            }
        }
        else
        {
            _InteractableText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            other.gameObject.GetComponent<IInteractable>().Interact(_playerData);
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
