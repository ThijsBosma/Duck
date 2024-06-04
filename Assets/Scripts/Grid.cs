using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Grid : InputHandler
{
    [Header("Grid")]
    [SerializeField] private MeshRenderer _Ground;

    [Header("Grid sizes")]
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _cellSize;

    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask ground;

    [SerializeField] private bool _showDebug;

    private Vector3 mouseClick;

    private int[,] _gridArray;
    private TextMesh[,] _debugTextArray;
    private bool _textActive;

    private void Start()
    {
        GetMeshSize();
        ConstructGrid();

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

            Setvalue(hit.point, 69);
        }

        if (_Input.UI.RightClick.WasPressedThisFrame())
        {
            Vector2 mousePos = _Input.UI.Point.ReadValue<Vector2>();
            Ray ray = cam.ScreenPointToRay(mousePos);

            RaycastHit hit;
            Physics.Raycast(ray, out hit, ground);

            Debug.Log(GetValue(hit.point));
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
                _debugTextArray[x, z] = CreateWorldText(transform, _gridArray[x, z].ToString(), GetWorldPosition(x, z) + new Vector3(_cellSize, 0, _cellSize) * 0.5f, 20, Color.white);
            }
        }
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
        GetXZ(worldPosition, out x, out z);
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
        GetXZ(worldPosition, out x, out z);
        return GetValue(x, z);
    }

    public Vector3 GetWorldPosition(float x, float z)
    {
        return new Vector3(x, transform.position.y, z) * _cellSize + new Vector3(transform.position.x, 0, transform.position.z);
    }

    public void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - transform.position).x / _cellSize);
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
            if (!_textActive)
            {
                for (int j = 0; j < _debugTextArray.GetLength(0); j++)
                {
                    for (int i = 0; i < _debugTextArray.GetLength(1); i++)
                    {
                        _debugTextArray[j, i].gameObject.SetActive(true);
                    }
                }

                _textActive = true;
            }

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
        else if (_textActive)
        {
            for (int j = 0; j < _debugTextArray.GetLength(0); j++)
            {
                for (int i = 0; i < _debugTextArray.GetLength(1); i++)
                {
                    _debugTextArray[j, i].gameObject.SetActive(false);
                }
            }
            _textActive = false;
        }
    }
}
