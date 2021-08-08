using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathFinding : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates{get{return startCoordinates;}}

    [SerializeField] Vector2Int endCoordinates;
    public Vector2Int EndCoordinates{get{return endCoordinates;}}




    Node startNode;
    Node destinationNode;
    Node CurrentNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = { Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;

            startNode = grid[startCoordinates];
            destinationNode = grid[endCoordinates];



        }



    }
    private void Start()
    {
        getNewPath();

    }



    public List<Node> getNewPath()
    {
        gridManager.ResetNode();
        breathFirstSearch();
        return buildPath();
    }

    void FindNeighbours()
    {
        List<Node> neighBoursNodes = new List<Node>();

        foreach(Vector2Int direction in directions)
        {
            Vector2Int Neighbour = CurrentNode.coordinates + direction;
            if (grid.ContainsKey(Neighbour))
            {
                neighBoursNodes.Add(grid[Neighbour]);



            }

        }

        foreach(Node neighbour in neighBoursNodes)
        {
            if(!reached.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
            {
                neighbour.connectedNode = CurrentNode;
                reached.Add(neighbour.coordinates, neighbour);
                frontier.Enqueue(neighbour);
            }
        }

    }

    void breathFirstSearch()
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();
        bool isRunning = true;


        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);
        while (frontier.Count > 0 && isRunning)
        {
            CurrentNode = frontier.Dequeue();
            CurrentNode.isExplored = true;
            FindNeighbours();
            if(CurrentNode.coordinates == endCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> buildPath()
    {
        List <Node>  path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;



        while(currentNode.connectedNode != null)
        {
            currentNode = currentNode.connectedNode;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();
        return path;

    }

    public bool willBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable; 

            grid[coordinates].isWalkable = false;
            List<Node> newPath = getNewPath();
            grid[coordinates].isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                getNewPath();
                return true;
            }

        }

        return false;
    }

}
