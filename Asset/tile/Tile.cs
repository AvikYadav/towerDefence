using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] Tower tower;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }


    GridManager gridManager;
    pathFinding pathFinder;

    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<pathFinding>();
    }
    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockedNode(coordinates);
                
            }
        }
    }
    void OnMouseDown()
    {
        if (gridManager.getNode(coordinates).isWalkable && !pathFinder.willBlockPath(coordinates))
        {
            bool isSuccessfull = tower.createTower(tower, transform.position);
            //Instantiate(tower, transform.position,Quaternion.identity
            isSuccessfull = !isSuccessfull;
            gridManager.BlockedNode(coordinates);
            
        }

    }


}
