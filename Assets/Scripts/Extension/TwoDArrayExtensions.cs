using System.Collections.Generic;

namespace TwoDArrayExtentions
{
    /// <summary>
    /// Extra custom methods for 2D arrays
    /// </summary>
    public static class TwoDArrayExtensions
    {
        /// <summary>
        /// Gets the index of target of 2D array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_grid"></param>
        /// <param name="_target"></param>
        /// <returns>
        /// Succes: Index of target from 2D array
        /// Failure: -1,-1 aka null.
        /// </returns>
        /// <remarks>cant write null or 0,0 in this case</remarks>
        public static (int, int) GetIndexOf2D<T>(this T[,] _grid, T _target) where T : class
        {
            // linear search this could be better with something like binary search
            // this is gonna be a problem soonish with the hunderds of nodes i am going to call
            for (int x = 0; x < _grid.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.GetLength(1); y++)
                {
                    if (_grid[x, y] == _target)
                    {
                        return (x, y);
                    }
                }
            }
            return (-1, -1);
        }

        /// <summary>
        /// Gets the direct neighbor of T target and returns null when index out of range or target itself.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_grid"></param>
        /// <param name="_target"></param>
        /// <param name="_XAxis"></param>
        /// <param name="_YAxis"></param>
        /// <returns>
        /// Returns target direct neighbor or null.
        /// </returns>
        public static T GetDirectNeigborNode<T>(this T[,] _grid, T _target, sbyte _XAxis, sbyte _YAxis) where T : class
        {
            (int x, int y) = _grid.GetIndexOf2D(_target);
            return GetTInArrayRange(_grid, x, y,_XAxis, _YAxis);
        }

        /// <summary>
        /// Extension method: Gets all available neigbors of target and returns as an array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_grid"></param>
        /// <param name="_target"></param>
        /// <returns>
        /// Direct neigbor in an array.
        /// </returns>
        public static T[] GetDirectNeigborNodes<T>(this T[,] _grid, T _target) where T : class
        {
            (int index_x, int index_y) = _grid.GetIndexOf2D(_target);

            List<T> Output = new List<T>();
            for (sbyte x = -1; x <= 1; x++)
            {
                for (sbyte y = -1; y <= 1; y++)
                {
                    T Neighbor = GetTInArrayRange(_grid, index_x, index_y, x, y);
                    if (Neighbor != null)
                    {
                        Output.Add(Neighbor);
                    }
                }
            }
            return Output.ToArray();

        }

        /// <summary>
        /// Checks if the index + x or y is still in range and returns T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_grid"></param>
        /// <param name="index_x"></param>
        /// <param name="index_y"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        /// Success: return T in array.
        /// Failure: returns null
        /// </returns>
        private static T GetTInArrayRange<T>(T[,] _grid, int index_x, int index_y, sbyte x = 0, sbyte y = 0) where T : class
        {
            return index_x + x >= 0
                         && index_x + x <= _grid.GetLength(0)
                         && index_y + y >= 0
                         && index_y + y <= _grid.GetLength(1) ? _grid[index_x + x, index_y + y] : null;
        }

    }
}