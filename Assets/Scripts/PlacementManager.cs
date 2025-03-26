using System;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] int width, height;

    [SerializeField] Transform buildingsParent;

    [SerializeField] GameObject natureFragment;
    [SerializeField] float radius;
    [SerializeField] float power;
    [SerializeField] float upforce;
    [SerializeField] LayerMask whatIsAffectedByExplosion;

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

    public void PlaceTemporaryStructure(Vector3Int position, GameObject structurePrefab, CellType type, int width = 1, int height = 1)
    {
        GameObject newStructure = Instantiate(structurePrefab, position, Quaternion.Euler(0, 180, 0));
        newStructure.transform.parent = buildingsParent;
        AudioManager.instance.PlayPlacementSound();

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var newPosition = position + new Vector3Int(x, 0, z);
                placementGrid[newPosition.x, newPosition.z] = type;
                DestroyNatureAt(newPosition);
            }
        } 
    }

    private void DestroyNatureAt(Vector3Int position)
    {
        RaycastHit[] hits = Physics.BoxCastAll(position + new Vector3(0, 0.5f, 0), new Vector3(0.5f, 0.5f, 0.5f), transform.up, Quaternion.identity, 1f, 1 << LayerMask.NameToLayer("Nature"));
        foreach (var item in hits)
        {
            Instantiate(natureFragment, new Vector3(item.transform.position.x, item.transform.position.y + 0.3f, item.transform.position.z + 0.3f), item.transform.rotation);
            Instantiate(natureFragment, new Vector3(item.transform.position.x + 0.3f, item.transform.position.y + 0.3f, item.transform.position.z - 0.15f), item.transform.rotation);
            Instantiate(natureFragment, new Vector3(item.transform.position.x - 0.3f, item.transform.position.y + 0.3f, item.transform.position.z - 0.15f), item.transform.rotation);
            Instantiate(natureFragment, new Vector3(item.transform.position.x + 0.15f, item.transform.position.y + 0.15f, item.transform.position.z + 0.3f), item.transform.rotation);
            Instantiate(natureFragment, new Vector3(item.transform.position.x - 0.15f, item.transform.position.y + 0.15f, item.transform.position.z - 0.3f), item.transform.rotation);
            Instantiate(natureFragment, new Vector3(item.transform.position.x, item.transform.position.y + 0.5f, item.transform.position.z + 0.3f), item.transform.rotation);
            Instantiate(natureFragment, new Vector3(item.transform.position.x - 0.5f, item.transform.position.y + 0.3f, item.transform.position.z - 0.15f), item.transform.rotation);
            Instantiate(natureFragment, new Vector3(item.transform.position.x + 0.5f, item.transform.position.y - 0.3f, item.transform.position.z + 0.15f), item.transform.rotation);
            Instantiate(natureFragment, new Vector3(item.transform.position.x + 0.5f, item.transform.position.y - 0.15f, item.transform.position.z - 0.3f), item.transform.rotation);
            Instantiate(natureFragment, new Vector3(item.transform.position.x - 0.5f, item.transform.position.y + 0.15f, item.transform.position.z - 0.3f), item.transform.rotation);

            Vector3 explosionPosition = item.transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius, whatIsAffectedByExplosion);
            foreach (Collider hit in colliders)
            {
                Rigidbody rigidB = hit.GetComponent<Rigidbody>();
                if (rigidB != null)
                {
                    rigidB.AddExplosionForce(power, explosionPosition, radius, upforce, ForceMode.Impulse);
                }
            }

            Destroy(item.collider.gameObject);
        }
    }
}
