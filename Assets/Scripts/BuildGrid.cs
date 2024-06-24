using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGrid : InputHandler
{
    [Header("Grid")]
    [SerializeField] private Collider _Ground;
    [SerializeField] private Transform[] BridgePlaces;
    public List<BridgeGridSpace> _BridgeGridSpaces = new List<BridgeGridSpace>();

    [Header("Grid sizes")]
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _cellSize;

    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask ground;

    [Header("Debug")]
    [SerializeField] private Transform DebugText;
    [SerializeField] private bool _showDebug;

    private Vector3 mouseClick;

    private int[,] _gridArray;
    private TextMesh[,] _debugTextArray;
    private bool _isTextCreated;

    protected override void Awake()
    {
        base.Awake();
        GetMeshSize();
        ConstructGrid();
        if (BridgePlaces.Length > 0)
            SetBridgePlaces();
    }

    private void Start()
    {
        _Input.UI.Enable();
    }

    private void Update()
    {
        if (_Input.UI.Click.WasPressedThisFrame())
        {
            Vector2 mousePos = _Input.UI.Point.ReadValue<Vector2>();
            Ray ray = cam.ScreenPointToRay(mousePos);

            RaycastHit hit;
            Physics.Raycast(ray, out hit, ground);

            mouseClick = hit.point;
        }

        if (_Input.UI.RightClick.WasPressedThisFrame())
        {
            Vector2 mousePos = _Input.UI.Point.ReadValue<Vector2>();
            Ray ray = cam.ScreenPointToRay(mousePos);

            RaycastHit hit;
            Physics.Raycast(ray, out hit, ground);
        }
    }

    protected void ConstructGrid()
    {
        _gridArray = new int[_width, _height];
        _debugTextArray = new TextMesh[_width, _height];

        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < _gridArray.GetLength(1); z++)
            {
                if (!_isTextCreated)
                    _debugTextArray[x, z] = CreateWorldText(DebugText.transform, _gridArray[x, z].ToString(), GetWorldPosition(x, z) + new Vector3(_cellSize, 0, _cellSize) * 0.5f, 20, Color.white);
            }
        }
        _isTextCreated = true;
    }

    public void SetValue(int x, int z, int value)
    {
        if (x >= 0 && z >= 0 && x < _width && z < _height)
        {
            _gridArray[x, z] = value;
            _debugTextArray[x, z].text = _gridArray[x, z].ToString();
        }
    }

    public void Setvalue(Vector3 worldPosition, int value)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z, false);
        SetValue(x, z, value);
    }

    public int GetValue(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < _width && z < _height)
            return _gridArray[x, z];
        else
            return -1;
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z, false);
        return GetValue(x, z);
    }

    public Vector3 GetWorldPosition(float x, float z)
    {
        return new Vector3(x, transform.position.y, z) * _cellSize + new Vector3(transform.position.x, 0, transform.position.z);
    }

    public void GetXZ(Vector3 worldPosition, out int x, out int z, bool isBridgeOffsetObject)
    {
        float xTemp = worldPosition.x;
        float xFraction = xTemp - Mathf.Floor(xTemp);

        if (xFraction >= 0.5f && isBridgeOffsetObject)
            x = Mathf.RoundToInt((worldPosition - transform.position).x / _cellSize);
        else
        {
            int worldX = Mathf.FloorToInt(worldPosition.x);
            int gridX = Mathf.FloorToInt(transform.position.x);

            x = Mathf.FloorToInt((worldX - gridX) / _cellSize);
        }


        z = Mathf.FloorToInt((worldPosition - transform.position).z / _cellSize);
    }

    protected void GetMeshSize()
    {
        float xDistance = Mathf.Pow(Mathf.Abs(_Ground.bounds.min.x - _Ground.bounds.max.x), 2);
        float zDistance = Mathf.Pow(Mathf.Abs(_Ground.bounds.min.z - _Ground.bounds.max.z), 2);

        float xD = Mathf.Sqrt(xDistance);
        float zD = Mathf.Sqrt(zDistance);

        _width = Mathf.RoundToInt(xD);
        _height = Mathf.RoundToInt(zD);
    }

    private void SetBridgePlaces()
    {
        foreach (Transform transform in BridgePlaces)
        {
            GetXZ(transform.position, out int x, out int z, true);

            BridgeGridSpace gridSpace = transform.gameObject.GetComponent<BridgeOffset>()._OffsetGridSpace;
            gridSpace._GridSpace = new Vector2Int(x, z);

            _BridgeGridSpaces.Add(gridSpace);

            transform.position = GetWorldPosition(x, z) + new Vector3(_cellSize / 2, 0, _cellSize / 2);
        }
        
    }

    public Vector3 GetBridgeOffsetPosition(float x, float z)
    {
        foreach (BridgeGridSpace gridSpace in _BridgeGridSpaces)
        {
            if (gridSpace._GridSpace.x == x && gridSpace._GridSpace.y == z)
            {
                Transform transform = gridSpace.transform;
                Debug.Log("Bridge offset found on grid space");
                return transform.gameObject.GetComponent<BridgeOffset>()._offsetPosition;
            }
        }

        Debug.LogError("Bridge offset not found on grid space");
        return Vector3.zero;
    }

    public Vector3 GetParticleOffsetPosition(float x, float z)
    {
        foreach (BridgeGridSpace gridSpace in _BridgeGridSpaces)
        {
            if (gridSpace._GridSpace.x == x && gridSpace._GridSpace.y == z)
            {
                Transform transform = gridSpace.transform;
                Debug.Log("Particle offset place found on grid space");
                return transform.gameObject.GetComponent<BridgeOffset>()._ParticleOffset;
            }
        }

        Debug.LogError("Particle offset place not found on grid space");
        return Vector3.zero;
    }

    public Quaternion GetBridgeOffsetRotation(float x, float z)
    {
        foreach (BridgeGridSpace gridSpace in _BridgeGridSpaces)
        {
            if (gridSpace._GridSpace.x == x && gridSpace._GridSpace.y == z)
            {
                Transform transform = gridSpace.transform;
                Debug.Log("Bridge offset rotation found on grid space");
                return transform.gameObject.GetComponent<BridgeOffset>()._offsetRotation;
            }
        }

        Debug.LogError("Bridge offset rotaion not found on grid space");
        return Quaternion.identity;
    }

    public Vector3 GetBridgeOffsetSize(float x, float z)
    {
        foreach (BridgeGridSpace gridSpace in _BridgeGridSpaces)
        {
            if (gridSpace._GridSpace.x == x && gridSpace._GridSpace.y == z)
            {
                Transform transform = gridSpace.transform;
                Debug.Log("Bridge offset size found on grid space");
                Vector3 scale = transform.gameObject.GetComponent<BridgeOffset>()._offsetScale;
                if (scale.magnitude > Vector3.zero.magnitude)
                    return scale;
                else
                {
                    Debug.LogWarning("Bridge scale offset not set");
                    return Vector3.one;
                }
            }
        }

        Debug.LogError("Bridge offset size not found on grid space");
        return Vector3.zero;
    }

    public Transform GetBridgeOffsetTransform(float x, float z)
    {
        foreach (BridgeGridSpace gridSpace in _BridgeGridSpaces)
        {
            if (gridSpace._GridSpace.x == x && gridSpace._GridSpace.y == z)
            {
                Transform transform = gridSpace.transform;
                Debug.Log("Bridge offset transform found on grid space");
                return transform;
            }
        }

        Debug.LogError("Bridge offset transform not found on grid space");
        return null;
    }

    public bool CanBuild(Vector3 buildPosition)
    {
        return GetValue(buildPosition) == 0;
    }

    private TextMesh CreateWorldText(Transform parent, string text, Vector3 position, int fontsize, Color color)
    {
        GameObject gameObject = new GameObject("World text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent);
        transform.position = position;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Left;
        textMesh.characterSize = 0.2f;
        textMesh.text = text;
        textMesh.fontSize = fontsize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = 5000;
        return textMesh;
    }

    private void OnDrawGizmos()
    {
        if (_showDebug)
        {
            GetMeshSize();
            ConstructGrid();
            DebugText.gameObject.SetActive(true);

            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int z = 0; z < _gridArray.GetLength(1); z++)
                {
                    Gizmos.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1));
                    Gizmos.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z));
                }
            }
            Gizmos.DrawLine(GetWorldPosition(0, _width), GetWorldPosition(_width, _height));
            Gizmos.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height));

        }
        else if (!_showDebug)
        {
            DebugText.gameObject.SetActive(false);
        }
    }
}