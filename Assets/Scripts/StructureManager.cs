using UnityEngine;

/// <summary>
/// Provides various methods to validate structure placement
/// </summary>
public class StructureManager : MonoBehaviour
{
    [SerializeField] PlacementManager placementManager;

    /// <summary>
    /// Places a structure at the given position
    /// </summary>
    /// <param name="position">The position to place the structure</param>
    /// <param name="structurePrefab">The strucutre object to be placed</param>
    public void PlaceStructure(Vector3Int position, GameObject structurePrefab)
    {
        // Check if the position is valid
        if (CheckPosition(position))
        {
            placementManager.InstantiateStructure(position, structurePrefab, CellType.Structure);
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
            placementManager.InstantiateStructure(position, structurePrefab, CellType.Structure, width, height);
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
                if (!CheckPosition(newPosition))
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
    bool CheckPosition(Vector3Int position)
    {
        // Check if the position is inside the grid
        if (!placementManager.CheckIfPositionInBound(position))
        {
            Debug.Log("This position is out of bounds");
            return false;
        }

        // Check if the position is not occupied by another structure
        if (!placementManager.CheckIfPositionIsFree(position))
        {
            Debug.Log("This position is not free");
            return false;
        }
        return true;
    }
}
