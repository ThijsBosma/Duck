using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTree : FindInputBinding
{
    [SerializeField] private GameObject tree;
    public GameObject _Seed;

    [SerializeField] private Transform _ShootRayPos;

    [SerializeField] private LayerMask _PlantLayer;

    private PlayerPickUp _playerPickup;

    private Transform _plantPos;

    private bool _inputIsActive;

    // Start is called before the first frame update
    void Start()
    {
        _playerPickup = GetComponent<PlayerPickUp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerData._Instance._SeedPickedup == 1)
        {
            if (_Plant.IsPressed())
            {
                if (!_inputIsActive)
                {
                    _inputIsActive = true;
                    _Interact.Enable();

                    SetText("to plant the seed", true);
                }
            }
            else if (_inputIsActive)
            {
                _Interact.Disable();

                _inputIsActive = false;

                InteractText.instance.ResetText();
            }

            if (_Interact.WasPressedThisFrame())
            {
                RaycastHit hit;
                Physics.Raycast(_ShootRayPos.position, Vector3.down + _ShootRayPos.forward.normalized * 2f, out hit, _PlantLayer);

                Instantiate(tree, hit.point, Quaternion.identity);

                _playerPickup.ResetPickup();

                PlayerData._Instance._ObjectPickedup = 0;

                _Interact.Disable();

                Destroy(_Seed);
            }

            Debug.DrawRay(_ShootRayPos.position, Vector3.down + _ShootRayPos.forward.normalized * 2f);
        }
    }

    private void SetText(string text, bool needsBindingReference)
    {
        string controlScheme = playerInput.currentControlScheme;

        if (needsBindingReference)
        {
            if (controlScheme == "PlaystationController" || controlScheme == "XboxController" || controlScheme == "Gamepad")
            {
                InteractText.instance.SetText($"Press {FindIconBinding("Interact")} {text}");
            }
            else
            {
                InteractText.instance.SetText($"Press {FindBinding("Interact")} {text}");
            }
        }
        else
        {
            InteractText.instance.SetText($"{text}");
        }
    }

}
