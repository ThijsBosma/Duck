using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUp : FindInputBinding
{
    [Header("References")]
    [SerializeField] private Transform _Orientation;
    [SerializeField] private Image _InputIcon;
    private PlantTree _PlantTree;

    [Header("Pick Options")]
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _pickupLayer;

    private RaycastHit[] colliders;

    private IPickupable _Pickupable;

    private IPickupable _boxPickupable;
    private IPickupable _wateringCanPickupable;
    private IPickupable _seedPickupable;
    private IPickupable _getSeedPickupable;

    private int _buttonPresses;

    private bool _pickupableInRange;
    private bool _textHasReseted;
    private bool _isPickingUp;

    private void Start()
    {
        _PlantTree = GetComponent<PlantTree>();
    }

    private void Update()
    {
        if (_Pickup.WasPressedThisFrame())
        {
            _buttonPresses++;

            if (_buttonPresses == 1)
            {
                PlayerData._Instance._ObjectPicedkup = 1;

                _isPickingUp = true;
                if (_boxPickupable != null)
                {
                    _boxPickupable.PickUp();
                }
                else if (_wateringCanPickupable != null)
                {
                    PlayerData._Instance._WateringCanPickedup = 1;
                    _wateringCanPickupable.PickUp();
                }
                else if (_seedPickupable != null)
                {
                    PlayerData._Instance._SeedPickedup = 1;

                    _Plant.Enable();

                    _seedPickupable.PickUp();
                }
                else if (_getSeedPickupable != null)
                {
                    _getSeedPickupable.PickUp();
                }

                InteractText.instance.ResetText();
            }
            else if (_buttonPresses > 1)
            {
                if (_boxPickupable != null)
                {
                    _boxPickupable.LetGo();
                }
                else if (_wateringCanPickupable != null)
                {
                    PlayerData._Instance._WateringCanPickedup = 0;
                    _wateringCanPickupable.LetGo();
                }
                else if (_seedPickupable != null)
                {
                    PlayerData._Instance._SeedPickedup = 0;
                    _Plant.Disable();
                    _seedPickupable.LetGo();
                }

                PlayerData._Instance._ObjectPicedkup = 0;
                _isPickingUp = false;
            }
        }

        if (_buttonPresses > 1)
        {
            PlayerData._Instance._ObjectPicedkup = 0;

            _buttonPresses = 0;

            ResetPickup();

            if (_pickupableInRange == false)
                _Pickup.Disable();
        }

        bool hasColliders = colliders.Length > 0;

        if (hasColliders)
        {
            HandlePickup();
        }
        else if (!_isPickingUp)
        {
            ResetPickup();
        }
    }

    private void FixedUpdate()
    {
        if (PlayerData._Instance._ObjectPicedkup == 0)
        {
            colliders = Physics.SphereCastAll(transform.position, _radius, _Orientation.forward, 0f, _pickupLayer);

            foreach (RaycastHit hit in colliders)
            {
                IPickupable pickupable = hit.collider.gameObject.GetComponent<IPickupable>();

                if (pickupable != null)
                {
                    if (pickupable is BoxPickup)
                        _boxPickupable = pickupable;
                    else if (pickupable is WateringCan)
                        _wateringCanPickupable = pickupable;
                    else if (pickupable is SeedPickup)
                    {
                        _seedPickupable = pickupable;
                        _PlantTree._Seed = hit.collider.gameObject;
                    }
                    else if (pickupable is GetSeedBack)
                    {
                        Debug.Log("Mega gay");
                        _getSeedPickupable = pickupable;
                    }
                }
            }
        }
    }

    private void HandlePickup()
    {
        if (!_isPickingUp)
        {
            if (_boxPickupable != null)
                HandleBoxPickup();
            else if (_wateringCanPickupable != null)
                HandleWateringCanPickup();
            else if (_seedPickupable != null)
                HandleSeedPickUp();
            else if (_getSeedPickupable != null)
                HandleGetSeedPickUp();
        }
    }

    private void HandleBoxPickup()
    {
        _Pickup.Enable();
        _pickupableInRange = true;

        SetText("to lift the box", true);
    }

    private void HandleWateringCanPickup()
    {
        _Pickup.Enable();
        _pickupableInRange = true;

        SetText("to lift the watering can", true);
    }

    private void HandleSeedPickUp()
    {
        _Pickup.Enable();
        _pickupableInRange = true;

        SetText("to lift the seed", true);
    }

    private void HandleGetSeedPickUp()
    {
        bool playerIsHoldingObject = PlayerData._Instance._ObjectPicedkup == 0;

        if (playerIsHoldingObject)
        {
            SetText("Hands full", false);
            _pickupableInRange = true;
        }
        else
        {
            _Pickup.Enable();
            _pickupableInRange = true;

            SetText("to dig up the seed", true);
        }
    }

    private void SetText(string text, bool needsBindingReference)
    {
        string controlScheme = playerInput.currentControlScheme;

        if (needsBindingReference)
        {
            if (controlScheme == "PlaystationController" || controlScheme == "XboxController" || controlScheme == "Gamepad")
            {
                InteractText.instance.SetText($"Press {FindIconBinding("Pickup")} {text}");
            }
            else
            {
                InteractText.instance.SetText($"Press {FindBinding("Pickup")} {text}");
            }
        }
        else
        {
            InteractText.instance.SetText($"{text}");
        }

        _textHasReseted = false;
    }

    private void ResetPickup()
    {
        //Disable pickup
        _Pickup.Disable();
        _pickupableInRange = false;

        Debug.Log("gay");

        //Reset interaction text only once
        if (!_textHasReseted)
        {
            InteractText.instance.ResetText();
            _textHasReseted = true;
        }

        //Reset pickupables to null
        _boxPickupable = null;
        _wateringCanPickupable = null;
        _seedPickupable = null;
        _getSeedPickupable = null;
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
                    string controlScheme = playerInput.currentControlScheme;
                    if (controlScheme == "PlaystationController" || controlScheme == "Gamepad" || controlScheme == "XboxController")
                    {
                        InteractText.instance.SetText($"Press {FindIconBinding("Pickup")} to pick up");
                    }
                    else if (playerInput.currentControlScheme == "Keyboard&Mouse")
                    {
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
