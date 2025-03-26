using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StructureManager : MonoBehaviour
{
    [SerializeField] PlacementManager placementManager;

    public void PlaceStructure(Vector3Int position, GameObject structurePrefab)
    {
        if (CheckPosition(position))
        {
            placementManager.PlaceTemporaryStructure(position, structurePrefab, CellType.Structure);
        }
    }

    public void PlaceBigStructure(Vector3Int position, GameObject structurePrefab)
    {
        int width = 3;
        int height = 3;

        if (CheckBigStructure(position, width, height))
        {
            placementManager.PlaceTemporaryStructure(position, structurePrefab, CellType.Structure, width, height);
        }
    }

    bool CheckBigStructure(Vector3Int position, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3Int newPosition = position + new Vector3Int(x, 0, z);
                if (!CheckPosition(newPosition))
                    return false;
            }
        }
        return true;
    }

    bool CheckPosition(Vector3Int position)
    {
        if (!placementManager.CheckIfPositionInBound(position))
        {
            Debug.Log("This position is out of bounds");
            return false;
        }
            
        if (!placementManager.CheckIfPositionIsFree(position))
        {
            Debug.Log("This position is not free");
            return false;
        }
        return true;
    }
}
