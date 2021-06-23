using System.Collections.Generic;
using UnityEngine;
using AStar;

/// <summary>
/// Agent component.
/// Is used to generate a path based on its own position and target position and uses that path to go to the target.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class Agent : MonoBehaviour
{
    private CharacterController AgentController;
    private Grids GridObj;
    private Camera Cam;
    private List<Node> Path;

    [Range(0, 25)]
    public float Speed = 3;
    [Range(0, 25)]
    public float RotationSpeed = 3;


    // Start is called before the first frame update
    void Start()
    {
        // Gets nessasary components for start up.
        AgentController = gameObject.GetComponent<CharacterController>();
        GridObj = GameObject.FindGameObjectWithTag("AIGrid").GetComponent<Grids>();
        Cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if left mouse button is pressed to return true.
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Using the camera it shoots a laser getting a position out of it for then to call GoToTarget.
            Ray Ray = Cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(Ray, out RaycastHit Hit))
            {
                GoToTarget(Hit.point);
            }
        }
    }

    /// <summary>
    /// Generates path based on the currents agent position and the targets position.
    /// Unfinished, is meant to also meant to go to the target position instead of only generating the path.
    /// </summary>
    /// <param name="_targetPos"></param>
    private void GoToTarget(Vector3 _targetPos)
    {
        Node CurrentPos = GridObj.NodeFromWorldPoint(transform.position);
        Node TargetPos = GridObj.NodeFromWorldPoint(_targetPos);
        Debug.Log("Run Astar");
        Path = AStarCalculation.AStar(GridObj.GetGrid(), CurrentPos, TargetPos);
    }

    private void OnDrawGizmosSelected()
    {
        if (Path != null)
        {
            foreach (Node n in Path)
            {
                Gizmos.color = (n.Walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.WorldPosition, Vector3.one * (2 - .1f));
            }
        }
    }

}
