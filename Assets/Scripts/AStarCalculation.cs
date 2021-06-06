using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TwoDArrayExtentions;
using DictionaryExtensions;
using System;

public class AStarCalculation
{
    // for pseudocode
    // https://en.wikipedia.org/wiki/A*_search_algorithm
    public static List<Node> AStar(Node[,] _grid, Node _start, Node _end)
    {
        HashSet<Node> openSet = new HashSet<Node>() { _start };
        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();

        Dictionary<Node, float> gScore = Turn2DArrayToDict(_grid, float.MaxValue);
        gScore[_start] = 0;

        Dictionary<Node, float> fScore = Turn2DArrayToDict(_grid, float.MaxValue);
        fScore[_start] = H(_start, _end);

        while (openSet.Count != 0)
        {
            Node Current = GetNodeWithLowestScore(openSet, fScore);
            openSet.Remove(Current);
            if (Current == _end) return GeneratePath(cameFrom, Current);


            Node[] DirectNeighborNode = GetCurrentNeighborsArray(_grid, Current);
            foreach (var neighbor in DirectNeighborNode)
            {
                float Tentative_gScore = gScore[Current] + D(Current, neighbor);
                if (Tentative_gScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = Current;
                    gScore[neighbor] = Tentative_gScore;
                    fScore[neighbor] = gScore[neighbor] + H(neighbor, _end);
                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
        // failure path
        return null;
    }

    private static Node[] GetCurrentNeighborsArray(Node[,] _grid, Node _target)
    {
        List<Node> Output = new List<Node>();
        for (sbyte x = -1; x < 1; x++)
        {
            for (sbyte y = -1; y < 1; y++)
            {
                _grid.GetDirectNeigborNode(_target, x, y);
            }
        }
        return Output.ToArray();
    }

    private static Node GetNodeWithLowestScore(HashSet<Node> openSet, Dictionary<Node, float> fScore)
    {
        Node Output = null;
        var min = fScore.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
        if (openSet.Contains(min)) Output = min;
        return Output;
    }

    // D(current,neighbor) is the weight of the edge from current to neighbor
    // tentative_gScore is the distance from start to the neighbor through current
    private static float D(Node current, Node neighbor)
    {
        throw new NotImplementedException();
    }

    private static int H(Node _start, Node _end)
    {
        throw new NotImplementedException();
    }

    private static Dictionary<Node, float> Turn2DArrayToDict(Node[,] _grid, float _default)
    {
        Dictionary<Node, float> OutputDict = new Dictionary<Node, float>();

        for (int x = 0; x < _grid.GetLength(0); x++)
        {
            for (int y = 0; y < _grid.GetLength(1); y++)
            {
                OutputDict.Add(_grid[x, y], _default);
            }
        }
        return OutputDict;
    }

    private static List<Node> GeneratePath(Dictionary<Node, Node> _cameFrom, Node _current)
    {
        List<Node> GeneratePath = new List<Node>();

        return null;
    }
}