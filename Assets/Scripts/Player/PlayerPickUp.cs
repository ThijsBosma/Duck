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

    [SerializeField] private Transform _HoldPosition;
    [SerializeField] private Transform _PickupPosition;
    private PlantTree _PlantTree;

    [Header("Pick Options")]
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _pickupLayer;

    private RaycastHit[] colliders;

    private IPickupable _Pickupable;

    private IPickupable _wateringCanPickupable;
    private IPickupable _seedPickupable;
    private IPickupable _digUpSprout;

    public int _buttonPresses;

    private bool _pickupableInRange;
    private bool _hasReseted;

    public bool _isPickingUp;

    private string _pickupObject;

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
                PlayerData._Instance._ObjectPickedup = 1;

                if (_wateringCanPickupable != null)
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
                else if (_digUpSprout != null)
                {
                    _digUpSprout.PickUp();
                    _isPickingUp = false;
                    ResetPickup();
                }

                InteractText.instance.ResetText();
            }
            else if (_buttonPresses > 1)
            {
                if (_wateringCanPickupable != null)
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
            }
        }

        if (_buttonPresses > 1)
        {
            PlayerData._Instance._ObjectPickedup = 0;

            _buttonPresses = 0;

            ResetPickup();

            if (_pickupableInRange == false)
                _Pickup.Disable();
        }

        bool hasColliders = colliders.Length > 0;

        if (hasColliders)
        {
            HandlePickup();

            if (_hasReseted)
                _hasReseted = false;
        }
        else if (!_isPickingUp && !_hasReseted)
        {
            ResetPickup();
        }
    }

    private void FixedUpdate()
    {
        if (PlayerData._Instance._ObjectPickedup == 0)
        {
            colliders = Physics.SphereCastAll(transform.position, _radius, _Orientation.forward, 0f, _pickupLayer);

            foreach (RaycastHit hit in colliders)
            {
                IPickupable pickupable = hit.collider.gameObject.GetComponent<IPickupable>();

                if (pickupable != null)
                {
                    _pickupObject = pickupable.ToString();
                   
                    if (pickupable is WateringCan)
                        _wateringCanPickupable = pickupable;
                    else if (pickupable is SeedPickup)
                    {
                        _seedPickupable = pickupable;
                        if (_PlantTree._Seed == null)
                        {
                            _PlantTree._Seed = hit.collider.gameObject;
                        }
                    }
                    else if (pickupable is SproutPickup)
                    {
                        SproutPickup sprout = hit.collider.gameObject.GetComponent<SproutPickup>();
                        sprout._HoldPosition = _HoldPosition;
                        sprout._PickupPosition = _PickupPosition;

                        _digUpSprout = pickupable;
                    }
                }
            }
        }
    }

    private void HandlePickup()
    {
        if (!_isPickingUp)
        {
            if (_wateringCanPickupable != null)
                HandleWateringCanPickup();
            else if (_seedPickupable != null && _PlantTree._Seed != null)
                HandleSeedPickUp();
            else if (_digUpSprout != null)
                HandleGetSeedPickUp();
        }
    }

    private void HandleWateringCanPickup()
    {
        _Pickup.Enable();
        _pickupableInRange = true;

        SetText("to lift the watering can", true, "Pickup");
    }

    private void HandleSeedPickUp()
    {
        _Pickup.Enable();
        _pickupableInRange = true;

        SetText("to get seed back", true, "Pickup");
    }

    private void HandleGetSeedPickUp()
    {
        bool playerIsHoldingObject = PlayerData._Instance._ObjectPickedup == 0;

        if (!playerIsHoldingObject)
        {
            SetText("Hands full", false);
            _pickupableInRange = true;
        }
        else
        {
            Debug.Log("Pick seed");
            _Pickup.Enable();
            _pickupableInRange = true;

            SetText("to dig up the seed", true);
        }
    }

    public void ResetPickup()
    {
        //Disable pickup
        _Pickup.Disable();

        _pickupableInRange = false;
        _isPickingUp = false;

        _buttonPresses = 0;

        _PlantTree._Seed = null;

        _PlantTree._treeIndicator.SetActive(false);

        Debug.Log("Pickup reset");

        InteractText.instance.ResetText();
        _pickupObject = "";

        //Reset pickupables to null
        _wateringCanPickupable = null;
        _seedPickupable = null;
        _digUpSprout = null;

        _hasReseted = true;
    }

    private void OnDrawGizmos()
    {
        /*if (!_isPickingUp)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }*/
    }
}
