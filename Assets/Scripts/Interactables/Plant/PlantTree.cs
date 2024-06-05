using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTree : FindInputBinding
{
    [Header("Plant sprout")]
    [SerializeField] public GameObject _sprout;
    [SerializeField] public GameObject _treeIndicator;
    [SerializeField] private BuildGrid grid;
    public GameObject _Seed;

    private Vector3 _plantPosition;

    [SerializeField] private Transform _ShootRayPos;

    [SerializeField] private LayerMask _PlantLayer;

    private PlayerPickUp _playerPickup;

    private bool _inputIsActive;

    // Start is called before the first frame update
    void Start()
    {
        _playerPickup = GetComponent<PlayerPickUp>();

        if(grid != null)
        {
            grid.GetXZ(transform.position, out int x, out int z);

            Debug.Log(x + " " + z);

            transform.position = new Vector3(x, transform.position.y, z);

            grid.Setvalue(transform.position, 1);
        }
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

                if (grid == null)
                {
                    grid = hit.collider.gameObject.GetComponentInChildren<BuildGrid>();
                    _sprout.GetComponent<SproutPickup>()._grid = grid;
                }

                grid.GetXZ(hit.point, out int x, out int z);

                _plantPosition = grid.GetWorldPosition(x, z);
                if (grid.CanBuild(_plantPosition))
                {
                    _treeIndicator.SetActive(true);
                    MakeTreeHologram();
                }

                if (!_inputIsActive)
                {
                    _inputIsActive = true;
                    _Interact.Enable();

                    SetText("to plant the seed", true, "Interact");
                }
            }
            else if (_inputIsActive)
            {
                _Interact.Disable();

                _treeIndicator.SetActive(false);

                _inputIsActive = false;

                InteractText.instance.ResetText();
            }

            if (_Interact.WasPressedThisFrame())
            {
                if (grid.CanBuild(_plantPosition))
                {
                    grid.Setvalue(_plantPosition, 1);
                    Instantiate(_sprout, _plantPosition, Quaternion.identity);
                }

                _treeIndicator.SetActive(false);
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
        if (_treeIndicator.activeInHierarchy == false)
            _treeIndicator.SetActive(true);
        else
            _treeIndicator.transform.position = _plantPosition;
    }
}
