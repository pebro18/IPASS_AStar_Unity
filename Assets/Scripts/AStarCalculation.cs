using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TwoDArrayExtentions;

namespace AStar
{
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
                if (Current == _end)
                {
                    return GeneratePath(cameFrom, Current);
                }

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
            for (sbyte x = -1; x < 2; x++)
            {
                for (sbyte y = -1; y < 2; y++)
                {
                    Node Neighbor = _grid.GetDirectNeigborNode(_target, x, y);
                    if (Neighbor != null)
                    {
                        Output.Add(Neighbor);
                    }
                }
            }
            return Output.ToArray();
        }

        private static Node GetNodeWithLowestScore(HashSet<Node> _openSet, Dictionary<Node, float> _fScore)
        {
            Node Output = null;
            float lowestValue = float.MaxValue;
            foreach (var item in _openSet)
            {
                if (_fScore[item] < lowestValue)
                {
                    lowestValue = _fScore[item];
                    Output = item;
                }
            }
            return Output;
        }

        // D(current,neighbor) is the weight of the edge from current to neighbor
        // tentative_gScore is the distance from start to the neighbor through current

        // God IDK what this means ^^^^
        private static float D(Node current, Node neighbor)
        {
            float Distance = Vector3.Distance(current.WorldPosition, neighbor.WorldPosition);
            return (float)Math.Round(Distance, 2);
        }

        private static float H(Node _start, Node _end)
        {
            float Heuristic = Vector3.Distance(_start.WorldPosition, _end.WorldPosition);
            return (float)Math.Round(Heuristic, 2);
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
            List<Node> GeneratePath = new List<Node>() { _current};
            while (_cameFrom.Keys.Contains(_current))
            {
                _current = _cameFrom[_current];
                GeneratePath.Insert(0,_current);
            }
            return GeneratePath;
        }
    }
}