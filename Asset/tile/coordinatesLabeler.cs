using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class coordinatesLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color ExploreColor = Color.yellow;
    [SerializeField] Color pathColor = Color.black;
    [SerializeField] Color blockedColor = Color.grey;

    TextMeshPro label;
    Vector2Int coordinate = new Vector2Int();
    GridManager gridManager;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        displayCoordinates();

    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            displayCoordinates();
            updateObjectName();
            label.enabled = true;
        }

        colorCoordinates();
        ToggleLabels();

    }

    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void colorCoordinates()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.getNode(coordinate);
        
        
        if (node == null) { return; }
        
        
        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = ExploreColor;

        }
        else
        {
            label.color = defaultColor;
        }

    }

    void displayCoordinates()
    {
        if(gridManager == null) { return; }
        coordinate.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinate.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        label.text = $"{coordinate.x},{coordinate.y}";
    }

    void updateObjectName()
    {
        transform.parent.name = coordinate.ToString();
    }
}
