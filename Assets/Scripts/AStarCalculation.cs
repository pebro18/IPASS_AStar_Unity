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

        /// <summary>
        /// Main function for Astar Calculation.
        /// </summary>
        /// <param name="_grid"></param>
        /// <param name="_start"></param>
        /// <param name="_end"></param>
        /// <returns>
        /// Success: returns a List of Nodes.
        /// Failure: returns null.
        /// </returns>
        public static List<Node> AStar(Node[,] _grid, Node _start, Node _end)
        {
            HashSet<Node> openSet = new HashSet<Node>() { _start };
            Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();

            Dictionary<Node, float> gScore = Turn2DArrayToDict(_grid);
            gScore[_start] = 0;

            Dictionary<Node, float> fScore = Turn2DArrayToDict(_grid);
            fScore[_start] = DistanceBetween2Nodes(_start, _end);

            while (openSet.Count != 0)
            {
                Node Current = GetNodeWithLowestScore(openSet, fScore);
                openSet.Remove(Current);
                if (Current == _end)
                {
                    return GeneratePath(cameFrom, Current);
                }

                Node[] DirectNeighborNode = _grid.GetDirectNeigborNodes(Current);
                foreach (var neighbor in DirectNeighborNode)
                {
                    float Tentative_gScore = gScore[Current] + DistanceBetween2Nodes(Current, neighbor);
                    if (Tentative_gScore < gScore[neighbor] && neighbor.Walkable)
                    {
                        cameFrom[neighbor] = Current;
                        gScore[neighbor] = Tentative_gScore;
                        fScore[neighbor] = gScore[neighbor] + DistanceBetween2Nodes(neighbor, _end);
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

        /// <summary>
        /// Looks for the node with the lowest score that is available in the openSet
        /// </summary>
        /// <param name="_openSet"></param>
        /// <param name="_fScore"></param>
        /// <returns>
        /// Node with the lowest fScore in openSet
        /// </returns>
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

        /// <summary>
        /// Returns the distance between 2 nodes using the Nodes worldposition variable
        /// </summary>
        /// <param name="_start"></param>
        /// <param name="_end"></param>
        /// <returns>
        /// The distance of the 2 nodes as a float
        /// </returns>
        private static float DistanceBetween2Nodes(Node _start, Node _end)
        {
            float Heuristic = Vector3.Distance(_start.WorldPosition, _end.WorldPosition);
            return (float)Math.Round(Heuristic, 2);
        }

        /// <summary>
        /// Turn a 2D array into a Dict<Node,float> where an unspecified default inserts the float max value
        /// </summary>
        /// <param name="_grid"></param>
        /// <param name="_default"></param>
        /// <returns>
        /// Dictionary<Node,float> from 2D array
        /// </returns>
        private static Dictionary<Node, float> Turn2DArrayToDict(Node[,] _grid, float _default = float.MaxValue)
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

        /// <summary>
        /// Generates path by essentially back tracking from current node
        /// </summary>
        /// <param name="_cameFrom"></param>
        /// <param name="_current"></param>
        /// <returns>
        /// List<Node> Path
        /// </returns>
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