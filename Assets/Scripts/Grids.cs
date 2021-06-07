using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwoDArrayExtentions;

public class Grids : MonoBehaviour
{
    public LayerMask UnWalkable;
    public Vector2 gridWorldSize;
    public float nodeRadius;

    private Node[,] Grid;
    private float nodeDiamater;
    private int XGridSize, YGridSize;

    [SerializeField]
    private int XWorldNodePosOffset = 0;
    [SerializeField]
    private int YWorldNodePosOffset = -3;

    // Start is called before the first frame update
    void Start()
    {
        SetGridSize();
        CreateGrid();
    }

    // Calculates the radius of the grid system 
    private void SetGridSize()
    {
        nodeDiamater = nodeRadius * 2;
        XGridSize = Mathf.RoundToInt(gridWorldSize.x / nodeDiamater);
        YGridSize = Mathf.RoundToInt(gridWorldSize.y / nodeDiamater);
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
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, UnWalkable));
                Grid[x, y] = new Node(walkable, worldPoint);
            }
        }
    }

    // UI button function to reload Grid
    public void RegenGrid()
    {
        SetGridSize();
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

        Debug.Log($"PercentX: {percentX}");
        Debug.Log($"PercentY: {percentY}");

        int x = Mathf.RoundToInt((XGridSize - 1) * percentX) + XWorldNodePosOffset;
        int y = Mathf.RoundToInt((YGridSize - 1) * percentY) + YWorldNodePosOffset;

        Debug.Log($"X: {x}");
        Debug.Log($"Y: {y}");
        Node Output = Grid[x, y];
        return Output;
    }

    public Node[,] GetGrid()
    {
        return Grid;
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
