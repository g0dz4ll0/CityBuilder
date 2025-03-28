using UnityEngine;

/// <summary>
/// Provides various methods to validate structure placement
/// </summary>
public class StructureManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [Tooltip("The width of the grid")]
    [SerializeField] int width;
    [Tooltip("The height of the grid")]
    [SerializeField] int height;

    [HideInInspector]
    public Grid placementGrid;

    // Singleton pattern to make sure there is only one instance of the StructureManager
    public static StructureManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Create a new grid with the given width and height
        placementGrid = new Grid(width, height);
    }

    /// <summary>
    /// Places a structure at the given position
    /// </summary>
    /// <param name="position">The position to place the structure</param>
    /// <param name="structurePrefab">The strucutre object to be placed</param>
    public void PlaceStructure(Vector3Int position, GameObject structurePrefab)
    {
        // Check if the position is valid
        if (CheckStructure(position))
        {
            PlacementManager.instance.InstantiateStructure(position, structurePrefab, CellType.Structure);
        }
    }

    /// <summary>
    /// Places a big structure at the given position
    /// </summary>
    /// <param name="position">The position to place the structure</param>
    /// <param name="structurePrefab">The structure object to be placed</param>
    public void PlaceBigStructure(Vector3Int position, GameObject structurePrefab)
    {
        // The width and height that the big structure occupies
        int width = 3;
        int height = 3;

        // Check if the position is valid (here we pass the width and height as parameters)
        if (CheckBigStructure(position, width, height))
        {
            PlacementManager.instance.InstantiateStructure(position, structurePrefab, CellType.Structure, width, height);
        }
    }

    /// <summary>
    /// Checks if the big structure can be placed at the given position
    /// </summary>
    /// <param name="position">The position to place the structure</param>
    /// <param name="width">The width of the structure</param>
    /// <param name="height">The height of the structure</param>
    /// <returns>True if it can be placed, false if it can't be placed</returns>
    bool CheckBigStructure(Vector3Int position, int width, int height)
    {
        // Loop through the width and height of the structure
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                // Calculate the new position and check if it is valid
                Vector3Int newPosition = position + new Vector3Int(x, 0, z);
                if (!CheckStructure(newPosition))
                    return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Checks if the given position is valid
    /// </summary>
    /// <param name="position">The position to check</param>
    /// <returns>True if it is valid, false if it isn't</returns>
    bool CheckStructure(Vector3Int position)
    {
        // Check if the position is inside the grid
        if (!CheckIfPositionInBound(position))
        {
            Debug.Log("This position is out of bounds");
            return false;
        }

        // Check if the position is not occupied by another structure
        if (!CheckIfPositionIsFree(position))
        {
            Debug.Log("This position is not free");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the position is inside the grid
    /// </summary>
    /// <param name="position">The position to check</param>
    /// <returns>True if it is inside the grid, false if it isn't</returns>
    public bool CheckIfPositionInBound(Vector3Int position)
    {
        if (position.x >= 0 && position.x < width && position.z >= 0 && position.z < height)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks if the position is not occupied by another structure
    /// </summary>
    /// <param name="position">The position to check</param>
    /// <returns>True if it is not occupied (of the type Empty), false is it is occupied (any other type)</returns>
    public bool CheckIfPositionIsFree(Vector3Int position)
    {
        return CheckIfPositionIsOfType(position, CellType.Empty);
    }

    /// <summary>
    /// Checks if the position is of the given type, used to check if the position is free or not.
    /// </summary>
    /// <param name="position">The cells position</param>
    /// <param name="type">The current type of cell in the given position</param>
    /// <returns>True if the type matches the given type, false if it doesn't</returns>
    bool CheckIfPositionIsOfType(Vector3Int position, CellType type)
    {
        return placementGrid[position.x, position.z] == type;
    }
}
