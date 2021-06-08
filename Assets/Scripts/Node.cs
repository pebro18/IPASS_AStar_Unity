using UnityEngine;

/// <summary>
/// Simple class that holds its position in the world and walkability
/// </summary>
public class Node
{
    public bool Walkable;
    public Vector3 WorldPosition;

    public Node(bool _walkable, Vector3 _worldPos)
    {
        Walkable = _walkable;
        WorldPosition = _worldPos;
    }

}
