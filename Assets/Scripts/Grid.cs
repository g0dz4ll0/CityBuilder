/// <summary>
/// The type of cell in the grid
/// </summary>
public enum CellType
{
    Empty,
    Structure,
    None
}

/// <summary>
/// A class that represents a grid of cells
/// </summary>
public class Grid
{
    CellType[,] _grid; // A grid is a 2D array of CellTypes

    // The width and height of the grid
    int _width;
    public int Width { get { return _width; } }
    int _height;
    public int Height { get { return _height; } }

    public Grid(int width, int height)
    {
        _width = width;
        _height = height;
        _grid = new CellType[width, height];
    }

    // Indexer to access the grid
    public CellType this[int i, int j]
    {
        get
        {
            return _grid[i, j];
        }
        set
        {
            _grid[i, j] = value;
        }
    }
}