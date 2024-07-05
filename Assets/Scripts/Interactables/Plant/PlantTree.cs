using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlantTree : FindInputBinding
{
    [Header("Plant sprout")]
    [SerializeField] public GameObject _sprout;
    [SerializeField] public GameObject _treeIndicator;
    [SerializeField] public MeshRenderer _treeIndicatorMesh;
    [SerializeField] private BuildGrid grid;
    public GameObject _Seed;

    [Header("Tree hologram")]
    [SerializeField] private Transform _ShootRayPos;
    [SerializeField] private GameObject[] _CoconutImages;

    [SerializeField] private LayerMask _PlantLayer;

    [Header("InputIcon")]
    [SerializeField] private GameObject _InputIcon;

    private Color _holowGramColor;

    private PlayerPickUp _playerPickup;

    private Vector3 _plantPosition;

    private Vector2Int _GridPosition;

    private bool _isHoldingSeed;

    // Start is called before the first frame update
    void Start()
    {
        _playerPickup = GetComponent<PlayerPickUp>();

        _holowGramColor = _treeIndicatorMesh.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerData._Instance._SeedPickedup == 1)
        {
            RaycastHit hit;
            Physics.SphereCast(_ShootRayPos.position, 0.5f,Vector3.down * 0.5f + _ShootRayPos.forward.normalized * 0.5f, out hit, _PlantLayer);

            grid.GetXZ(hit.point, out int x, out int z, false);

            _GridPosition = new Vector2Int(x, z);

            _plantPosition = grid.GetWorldPosition(x, z);
            MakeTreeHologram();

            if (grid.CanBuild(_plantPosition))
            {
                _treeIndicator.gameObject.SetActive(true);

                _InputIcon.SetActive(true);

                if (grid.GetValue(_plantPosition) == 0)
                {
                    _treeIndicatorMesh.material.color = _holowGramColor;
                }
            }
            else if (grid.GetValue(_plantPosition) == 1)
            {
                _InputIcon.SetActive(false);

                _treeIndicatorMesh.material.color = Color.red;
            }
            else if (grid.GetValue(_plantPosition) == -1)
            {
                _InputIcon.SetActive(false);

                _treeIndicator.SetActive(false);
            }

            if (!_isHoldingSeed)
            {
                _isHoldingSeed = true;
                _Interact.Enable();

                ChangeInputIcons._Instance.UpdateUIIcons(playerInput);
            }

            if (_Interact.WasPressedThisFrame())
            {
                if (grid.CanBuild(_plantPosition))
                {
                    grid.Setvalue(_plantPosition, 1);

                    Transform rotation = grid.GetBridgeOffsetTransform(_GridPosition.x, _GridPosition.y);

                    GameObject sprout = Instantiate(_sprout, grid.GetWorldPosition(_GridPosition.x, _GridPosition.y), quaternion.identity);
                    sprout.GetComponentInChildren<SproutPickup>()._grid = grid;
                    sprout.GetComponentInChildren<SproutPickup>()._plant._GridPosition = _GridPosition;

                    sprout.GetComponentInChildren<GrowPlant>().gameObject.transform.rotation = rotation.rotation;

                    foreach (GameObject coconut in _CoconutImages)
                    {
                        coconut.SetActive(false);
                    }

                    _treeIndicator.SetActive(false);
                    Destroy(_Seed);

                    _InputIcon.SetActive(false);

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
        else if (_isHoldingSeed)
        {
            _Interact.Disable();

            _treeIndicator.SetActive(false);

            _isHoldingSeed = false;
        }
    }

    private void MakeTreeHologram()
    {
        if (_treeIndicator.activeInHierarchy == false)
            _treeIndicator.SetActive(true);
        else
            _treeIndicator.transform.position = _plantPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_ShootRayPos.position + Vector3.down * 0.5f + _ShootRayPos.forward.normalized * 0.5f, 0.5f);
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
        _treeIndicatorMesh.material.color = _holowGramColor;
    }
}
