using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTree : FindInputBinding
{
    [Header("Plant sprout")]
    [SerializeField] public GameObject _sprout;
    [SerializeField] private GameObject _treeIndicator;
    [SerializeField] private Grid grid;
    public GameObject _Seed;
    public GameObject _treeHollowGram;

    private Vector3 _plantPosition;

    [SerializeField] private Transform _ShootRayPos;

    [SerializeField] private LayerMask _PlantLayer;

    private PlayerPickUp _playerPickup;

    private bool _inputIsActive;
    private bool _hollowgramSpawned;

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
                RaycastHit hit;
                Physics.Raycast(_ShootRayPos.position, Vector3.down + _ShootRayPos.forward.normalized * 2f, out hit, _PlantLayer);

                grid.GetXZ(hit.point, out int x, out int z);

                _plantPosition = grid.GetWorldPosition(x, z);
                MakeTreeHologram();

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

                Destroy(_treeHollowGram.gameObject);

                _inputIsActive = false;

                InteractText.instance.ResetText();
            }

            if (_Interact.WasPressedThisFrame())
            {
                Instantiate(_sprout, _plantPosition, Quaternion.identity);

                Destroy(_treeHollowGram.gameObject);
                Destroy(_Seed);

                _playerPickup.ResetPickup();

                PlayerData._Instance._ObjectPickedup = 0;

                _Interact.Disable();
            }

            Debug.DrawRay(_ShootRayPos.position, Vector3.down + _ShootRayPos.forward.normalized * 2f);
        }
    }

    private void MakeTreeHologram()
    {
        if (_treeHollowGram == null)
            _treeHollowGram = Instantiate(_treeIndicator, _plantPosition, Quaternion.identity);
        else
            _treeHollowGram.transform.position = _plantPosition;
    }
}
