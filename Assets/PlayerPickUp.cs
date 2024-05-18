using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUp : FindInputBinding
{
    [Header("References")]
    [SerializeField] private Transform _Orientation;
    [SerializeField] private Image _InputIcon;

    [Header("Pick Options")]
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _pickupLayer;

    private RaycastHit[] colliders;

    private IPickupable _Pickupable;

    private int _buttonPresses;

    private bool _pickupableInRange;
    private bool _textHasReseted;
    private bool _isPickingUp;

    private void Update()
    {
        if (_Pickup.WasPressedThisFrame())
        {
            foreach (RaycastHit hit in colliders)
            {
                if (_Pickupable == null)
                {
                    Debug.LogError($"Object '{hit.collider.name}' does not contain an IPickupable component");
                }
                else if (PlayerData._Instance._ObjectPicedkUp == 0)
                {
                    PlayerData._Instance._ObjectPicedkUp = 1;
                    _isPickingUp = true;

                    _Pickupable.PickUp();
                    _buttonPresses++;

                    InteractText.instance.ResetText();

                    Debug.Log($"Picked up {hit.collider.name}");
                }
                else if (PlayerData._Instance._ObjectPicedkUp == 1)
                {
                    _buttonPresses++;
                    _Pickupable.LetGo();
                    _isPickingUp = false;
                }
            }
        }

        if (_buttonPresses > 1)
        {
            PlayerData._Instance._ObjectPicedkUp = 0;

            _buttonPresses = 0;

            if(_pickupableInRange == false)
                _Pickup.Disable();
        }
    }

    private void FixedUpdate()
    {
        if (PlayerData._Instance._ObjectPicedkUp == 0)
        {
            colliders = Physics.SphereCastAll(transform.position, _radius, _Orientation.forward, 0f, _pickupLayer);
            CheckPickupInRange();

            if (_Pickupable == null)
            {
                foreach (RaycastHit hit in colliders)
                {
                    _Pickupable = hit.collider.gameObject.GetComponent<IPickupable>();
                    _Pickup.Enable();
                    break;
                }
            }
        }
    }

    private void CheckPickupInRange()
    {
        if (_buttonPresses < 1)
        {
            bool hasColliders = colliders.Length > 0;

            if (hasColliders && !_pickupableInRange)
            {
                _pickupableInRange = true;

                if (_Pickup.WasPressedThisFrame() ^ _pickupableInRange)
                {
                    if (playerInput.currentControlScheme == "PlaystationController" || playerInput.currentControlScheme == "Gamepad" || playerInput.currentControlScheme == "XboxController")
                    {
                        _InputIcon.sprite = FindIconBinding();

                        Color iconColor = _InputIcon.color;
                        iconColor.a = 255;
                        _InputIcon.color = iconColor;

                        InteractText.instance.SetText($"Press       to pick up");
                    }
                    else if (playerInput.currentControlScheme == "Keyboard&Mouse")
                    {
                        Debug.Log(FindBinding("Pickup"));
                        InteractText.instance.SetText($"Press {FindBinding("Pickup")} to pick up");
                    }
                    _textHasReseted = false;
                }
            }
            else if (!hasColliders && _pickupableInRange)
            {
                _pickupableInRange = false;

                _Pickupable = null;
                Debug.Log(_Pickupable);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!_isPickingUp)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
