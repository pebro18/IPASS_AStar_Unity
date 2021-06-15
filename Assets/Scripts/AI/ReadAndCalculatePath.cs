using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AStar;
using GameObjectExtensions;

public class ReadAndCalculatePath : MonoBehaviour
{
    [SerializeField] private SimplePathTranslationScriptableObject dataPassObj;
    private List<Node>[] Paths;
    private GameObject[] POIs;
    private Grids GridObj;
    private LineRenderer _LineRenderer;
    public Vector3 StartingPos, EndingPos;

    private void Start()
    {
        GridObj = GameObject.FindGameObjectWithTag("AIGrid").GetComponent<Grids>();
        POIs = gameObject.GetChildsObjs();
        _LineRenderer = GetComponent<LineRenderer>();

        LoadPositions();
    }

    public void LoadPositions()
    {
        List<Vector3> Positions = new List<Vector3>();
        foreach (var input in dataPassObj.Inputs)
        {
            foreach (var gameObj in POIs)
            {
                if (gameObj.name == input)
                {
                    Positions.Add(gameObj.transform.position);
                }
            }
        }

        StartPathsCalculation(Positions);
    }

    void StartPathsCalculation(List<Vector3> PositionsArray)
    {
        Paths = new List<Node>[PositionsArray.Count+1];
        Node CurrentPos = GridObj.NodeFromWorldPoint(StartingPos);
        Node TargetPos = GridObj.NodeFromWorldPoint(PositionsArray[0]);

        Paths[0] = AStarCalculation.AStar(GridObj.GetGrid(), CurrentPos, TargetPos);

        if (PositionsArray.Count == 1)
        {
            Paths[1] = AStarCalculation.AStar(GridObj.GetGrid(), CurrentPos, TargetPos);
        }
        else
        {
            for (int i = 0; i < PositionsArray.Count; i++)
            {
                CurrentPos = GridObj.NodeFromWorldPoint(PositionsArray[i]);
                if (i + 1 == PositionsArray.Count) TargetPos = GridObj.NodeFromWorldPoint(EndingPos);
                else TargetPos = GridObj.NodeFromWorldPoint(PositionsArray[i + 1]);
                Paths[i + 1] = AStarCalculation.AStar(GridObj.GetGrid(), CurrentPos, TargetPos);
            }
        }
        RenderPath();
    }

    void RenderPath()
    {
        int pointsAmount = 0;
        int lineIndex = 0;

        if (Paths == null) return;

        foreach (var path in Paths)
        {
            pointsAmount += path.Count;
        }
        _LineRenderer.positionCount = pointsAmount;

        foreach (var path in Paths)
        {
            for (int i = 0; i < path.Count; i++)
            {
                _LineRenderer.SetPosition(lineIndex, path[i].WorldPosition);
                lineIndex++;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Paths != null)
        {
            foreach (var nlist in Paths)
            {
                foreach (var n in nlist)
                {
                    Gizmos.color = (n.Walkable) ? Color.white : Color.red;
                    Gizmos.DrawCube(n.WorldPosition, Vector3.one * (2 - .1f));
                }
            }
        }
    }

}
