using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    [SerializeField] Vector2Int gridSize;
    [Tooltip("this should match unity grid snap settings")]
    [SerializeField] int unityGridSize = 10;
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    public int UnityGridSize { get { return unityGridSize; } }
    private void Awake()
    {
        creaateGrid();

    }

    public Node getNode(Vector2Int coordinate)
    {
        if (grid.ContainsKey(coordinate))
        {
            return grid[coordinate];
        }
        return null;
    }


    public void BlockedNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    public void ResetNode()
    {
        foreach(KeyValuePair<Vector2Int,Node> entry in grid)
        {
            entry.Value.connectedNode = null;
            entry.Value.isPath = false;
            entry.Value.isExplored = false;
        }
    }


    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinate = new Vector2Int();
        coordinate.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinate.y = Mathf.RoundToInt(position.z / unityGridSize);

        return coordinate;
    } 
    public Vector3 GetPositionFromCoordinates(Vector2Int coordinate)
    {
        Vector3 position = new Vector3();
        position.x = coordinate.x * unityGridSize;
        position.z = coordinate.y * unityGridSize;

        return position;
    }


    void creaateGrid()
    {
        for(int x = 0; x < gridSize.x;x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x,y);
                grid.Add(coordinates, new Node(coordinates,true));
                //Debug.Log(grid[coordinates].coordinates + " = " +grid[coordinates].isWalkable);
            }
        }
    }
}
