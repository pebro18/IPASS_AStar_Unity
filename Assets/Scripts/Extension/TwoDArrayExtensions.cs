
namespace TwoDArrayExtentions
{
    public static class TwoDArrayExtensions
    {
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

        public static T GetDirectNeigborNode<T>(this T[,] _grid, T _target, sbyte _XAxis, sbyte _YAxis) where T : class
        {
            (int x, int y) = _grid.GetIndexOf2D(_target);
            x += _XAxis;
            y += _YAxis;

            if (x >= 0 && x <= _grid.GetLength(0) && y >= 0 && y <= _grid.GetLength(1))
            {
                T Output = _grid[x, y] != null && _target != _grid[x, y] ? _grid[x, y] : null;
                return Output;
            }
            return null;

        }
    }
}