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
        RenderPath();

    }

    /// <summary>
    /// Load the positions of POI from the dataPassObj scriptableobject
    /// </summary>
    private void LoadPositions()
    {
        List<Vector3> FoundPositions = new List<Vector3>();

        foreach (var input in dataPassObj.Inputs)
        {
            foreach (var gameObj in POIs)
            {
                if (gameObj.name == input)
                {
                    FoundPositions.Add(gameObj.transform.position);
                }
            }
        }

        GreedyFirstSort(FoundPositions);
    }
    /// <summary>
    /// Sorts the Positions list based on the shortest distance.
    /// Based on greedy first.
    /// </summary>
    /// <param name="FoundPositions"></param>
    private void GreedyFirstSort(List<Vector3> FoundPositions)
    {
        List<Vector3> Positions = new List<Vector3>();
        int Count = FoundPositions.Count;
        Vector3 Current = StartingPos;
        for (int i = 0; i < Count; i++)
        {
            Vector3 smallestDistance = GetVector3WithSmallestDistance(Current, FoundPositions);
            Positions.Add(smallestDistance);
            FoundPositions.Remove(smallestDistance);
            Current = smallestDistance;
        }
        StartPathsCalculation(Positions);
    }

    /// <summary>
    /// Find the closest position from Target and the availible positions
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="Positions"></param>
    /// <returns>Returns the closest position from target</returns>
    Vector3 GetVector3WithSmallestDistance(Vector3 Target, List<Vector3> Positions)
    {
        Vector3 smallestDistance = Vector3.zero;
        float minDistance = float.MaxValue;
        foreach (var pos in Positions)
        {

            float Heuristic = Vector3.Distance(Target, pos);
            if (pos == Target) break;

            if (Heuristic < minDistance)
            {
                smallestDistance = pos;
                minDistance = Heuristic;
            }
        }
        return smallestDistance;
    }

    /// <summary>
    /// Calculates all paths given by Positions list
    /// </summary>
    /// <param name="PositionsArray"></param>
    void StartPathsCalculation(List<Vector3> PositionsArray)
    {
        Paths = new List<Node>[PositionsArray.Count + 1];
        Node CurrentPos = GridObj.NodeFromWorldPoint(StartingPos);
        Node TargetPos = GridObj.NodeFromWorldPoint(PositionsArray[0]);

        Paths[0] = AStarCalculation.AStar(GridObj.GetGrid(), CurrentPos, TargetPos);

        if (PositionsArray.Count == 1)
        {
            Paths[1] = AStarCalculation.AStar(GridObj.GetGrid(), TargetPos, GridObj.NodeFromWorldPoint(EndingPos));
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

    }

    /// <summary>
    /// Renders the path with use of the linerenderer
    /// </summary>
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
    /// <summary>
    /// Reloads the whole path.
    /// used if are variables changed
    /// </summary>
    public void ReloadPath()
    {
        LoadPositions();
        RenderPath();
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
