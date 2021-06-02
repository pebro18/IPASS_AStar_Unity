
namespace TwoDArrayExtentions
{
    public static class TwoDArrayExtensions
    {
        public static (int,int) GetIndexOf2D<T>(this T[,] _grid, T _target) where T : class
        {
            // linear search this could be better with something like binary search
            // this is gonna be a problem soonish with the hunderds of nodes i am going to call
            for (int x = 0; x < _grid.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.GetLength(1); y++)
                {
                    if (_grid[x, y] == _target)
                    {
                        return (x,y);
                    }
                }
            }
            return (-1,-1);
        }
    }
}