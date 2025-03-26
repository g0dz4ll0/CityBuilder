using System;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] int width, height;
    Grid placementGrid;

    void Start()
    {
        placementGrid = new Grid(width, height);
    }

    public bool CheckIfPositionInBound(Vector3Int position)
    {
        if(position.x >= 0 && position.x < width && position.z >= 0 && position.z < height)
        {
            return true;
        }
        return false;
    }

    public bool CheckIfPositionIsFree(Vector3Int position)
    {
        return CheckIfPositionIsOfType(position, CellType.Empty);
    }

    bool CheckIfPositionIsOfType(Vector3Int position, CellType type)
    {
        return placementGrid[position.x, position.z] == type;
    }

    public void PlaceTemporaryStructure(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        placementGrid[position.x, position.z] = type;
        GameObject newStructure = Instantiate(structurePrefab, position, Quaternion.Euler(0, 180, 0));
    }
}
