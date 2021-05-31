using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grids : MonoBehaviour
{
    public LayerMask unWalkable;
    public Vector2 gridWorldSize;
    public float nodeRadius;

    private Node[,] Grid;

    private float nodeDiamater;
    private int XGridSize, YGridSize;


    // Start is called before the first frame update
    void Start()
    {
        // Calculates the radius of the grid system 
        nodeDiamater = nodeRadius * 2;
        XGridSize = Mathf.RoundToInt(gridWorldSize.x / nodeDiamater);
        YGridSize = Mathf.RoundToInt(gridWorldSize.y / nodeDiamater);
        CreateGrid();
    }

    // Create Nodes on the grid and check if there are obstuctions
    private void CreateGrid()
    {
        Grid = new Node[XGridSize, YGridSize];
        Vector3 WorldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < XGridSize; x++)
        {
            for (int y = 0; y < YGridSize; y++)
            {
                Vector3 worldPoint = WorldBottomLeft + Vector3.right * (x * nodeDiamater + nodeRadius) + Vector3.forward * (y * nodeDiamater + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unWalkable));
                Grid[x, y] = new Node(walkable, worldPoint);
            }
        }
    }

    // UI button function to reload Grid
    public void RegenGrid()
    {
        CreateGrid();
    }

    // gets node based on world position
    // is needed later
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((XGridSize - 1) * percentX);
        int y = Mathf.RoundToInt((YGridSize - 1) * percentY);
        return Grid[x, y];
    }

    // renders cubes to see in editor
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (Grid != null)
        {
            foreach (Node n in Grid)
            {
                Gizmos.color = (n.Walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.WorldPosition, Vector3.one * (nodeDiamater - .1f));
            }
        }
    }
}
