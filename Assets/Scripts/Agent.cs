using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AStar;

[RequireComponent(typeof(CharacterController))]
public class Agent : MonoBehaviour
{
    CharacterController AgentController;
    Grids GridObj;
    Camera Cam;
    List<Node> Path;

    [Range(0, 25)]
    public float Speed = 3;
    [Range(0, 25)]
    public float RotationSpeed = 3;


    // Start is called before the first frame update
    void Start()
    {
        AgentController = gameObject.GetComponent<CharacterController>();
        GridObj = GameObject.FindGameObjectWithTag("AIGrid").GetComponent<Grids>();
        Cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray Ray = Cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(Ray, out RaycastHit Hit))
            {
                GoToTarget(Hit.point);
            }
        }
    }

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
