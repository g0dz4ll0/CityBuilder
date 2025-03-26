using System.Collections.Generic;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    [SerializeField] PlacementManager placementManager;
    [SerializeField] List<Vector3Int> temporaryPlacementPositions = new List<Vector3Int>();

    public void PlaceStructure(Vector3Int position, GameObject structurePrefab)
    {
        if (placementManager.CheckIfPositionInBound(position) == false)
            return;
        if (placementManager.CheckIfPositionIsFree(position) == false)
            return;
        placementManager.PlaceTemporaryStructure(position, structurePrefab, CellType.Structure);
    }
}
