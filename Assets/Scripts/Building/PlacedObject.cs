using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlacedObject : ScriptableObject
{
    public enum Dir
    {
        Up,
        Down,
        Left,
        Right
    }

    public string objectName;
    public Transform prefab;
    public Transform visual;
    public int width;
    public int length;

    public List<Vector2Int> GetGridPositions(Vector2Int offset, Dir dir)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();
        switch (dir)
        {
            default:
            case Dir.Down:
            case Dir.Up:
                for (int x = 0; x < width; x++)
                {
                    for (int z = 0; z < length; z++)
                    {
                        gridPositionList.Add(offset + new Vector2Int(x, z));
                    }
                }
                break;
            case Dir.Left:
            case Dir.Right:
                for (int x = 0; x < length; x++)
                {
                    for (int z = 0; z < width; z++)
                    {
                        gridPositionList.Add(offset + new Vector2Int(x, z));
                    }
                }
                break;
        }
        return gridPositionList;
    }
}
