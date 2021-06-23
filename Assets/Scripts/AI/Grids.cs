using UnityEngine;

/// <summary>
/// An mayor component for the AI and Pathfinding.
/// This script has to be assigned as an component in a GameObject to allow pathfinding to function
/// </summary>
public class Grids : MonoBehaviour
{
    public LayerMask UnWalkable;
    public Vector2 gridWorldSize = new Vector2(1,1);
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

    /// <summary>
    /// Calculates the NodeDiameter and grid size based on gridWorldSize and nodeRadius.
    /// </summary>
    private void SetGridSize()
    {
        nodeDiamater = nodeRadius * 2;
        XGridSize = Mathf.RoundToInt(gridWorldSize.x / nodeDiamater);
        YGridSize = Mathf.RoundToInt(gridWorldSize.y / nodeDiamater);
    }

    /// <summary>
    /// Create Nodes on the grid based on the variables set by SetGridSize and checks if there are obstuctions
    /// </summary>
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

    /// <summary>
    /// Public method to reload the grid if changes are made
    /// </summary>
    public void RegenGrid()
    {
        SetGridSize();
        CreateGrid();
    }

    /// <summary>
    /// Returns a node based on the position given.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns>Node that corresponds to the worldposition given</returns>
    /// <remarks>
    /// There is a rounding issue when calling this function.
    /// Use XWorldNodePosOffset and YWorldNodePosOffset to offset any rounding error made.
    /// </remarks>
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((XGridSize - 1) * percentX) + XWorldNodePosOffset;
        int y = Mathf.RoundToInt((YGridSize - 1) * percentY) + YWorldNodePosOffset;
        Node Output = Grid[x, y];
        return Output;
    }

    /// <summary>
    /// Gets the grid variable when called
    /// </summary>
    /// <returns>Node[,] containing the level's nodes</returns>
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
