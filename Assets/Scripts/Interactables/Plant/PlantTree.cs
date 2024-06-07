using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTree : FindInputBinding
{
    [Header("Plant sprout")]
    [SerializeField] private PlacedObject _PlacedObject;
    [SerializeField] public GameObject _sprout;
    [SerializeField] public GameObject _treeIndicator;
    [SerializeField] private BuildGrid grid;
    public GameObject _Seed;

    private Vector3 _plantPosition;

    [SerializeField] private Transform _ShootRayPos;

    [SerializeField] private LayerMask _PlantLayer;

    [SerializeField] private Material _HoloGramMaterial;
    private Color _holowGramColor;

    private PlayerPickUp _playerPickup;

    private bool _inputIsActive;

    // Start is called before the first frame update
    void Start()
    {
        _playerPickup = GetComponent<PlayerPickUp>();

        _holowGramColor = _HoloGramMaterial.color;
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

                if (grid.CanBuild(_plantPosition))
                {
                    _treeIndicator.SetActive(true);
                    if (grid.GetValue(_plantPosition) == 0)
                    {
                        _HoloGramMaterial.color = _holowGramColor;
                    }
                }
                else if (!grid.CanBuild(_plantPosition))
                {
                    _HoloGramMaterial.color = Color.red;
                }
                else if (grid.GetValue(_plantPosition) == -1)
                {
                    _treeIndicator.SetActive(false);
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
                Debug.Log(grid.CanBuild(_plantPosition));

                if (grid.CanBuild(_plantPosition))
                {
                    grid.Setvalue(_plantPosition, 1);
                    GameObject sprout = Instantiate(_sprout, _plantPosition, Quaternion.identity);
                    sprout.GetComponentInChildren<SproutPickup>()._grid = grid;

                    _treeIndicator.SetActive(false);
                    Destroy(_Seed);

                    _playerPickup.ResetPickup();

                    PlayerData._Instance._ObjectPickedup = 0;

                    _Interact.Disable();
                }
                else
                {
                    Debug.LogError("You can't plant there dumbass");
                }
            }
        }
    }

    private void MakeTreeHologram()
    {
        if (_treeIndicator.activeInHierarchy == false)
            _treeIndicator.SetActive(true);
        else
            _treeIndicator.transform.position = _plantPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grid"))
        {
            grid = other.GetComponentInChildren<BuildGrid>();
        }
    }

    private void OnApplicationQuit()
    {
        _HoloGramMaterial.color = _holowGramColor;
    }
}
