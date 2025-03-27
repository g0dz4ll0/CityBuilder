using UnityEngine;

/// <summary>
/// Manages the placement of structures in the game
/// </summary>
public class PlacementManager : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The parent transform for the buildings in the hierarchy")]
    [SerializeField] Transform buildingsParent;

    [Header("Natures Destruction Settings")]
    [Tooltip("The nature fragment to instantiate when destroying nature")]
    [SerializeField] GameObject natureFragment;
    [Tooltip("The radius of the explosion")]
    [SerializeField] float radius;
    [Tooltip("The power of the explosion")]
    [SerializeField] float power;
    [Tooltip("The upforce of the explosion")]
    [SerializeField] float upforce;
    [Tooltip("The layer mask of the objects affected by the explosion")]
    [SerializeField] LayerMask whatIsAffectedByExplosion;

    // Singleton pattern to make sure there is only one instance of the PlacementManager
    public static PlacementManager instance;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position">The position to place the structure</param>
    /// <param name="structurePrefab">The prefab to be instantiated</param>
    /// <param name="type">The type of the structure</param>
    /// <param name="width">The width that the structure occupies (by default is 1)</param>
    /// <param name="height">The height that the structure occupies (by default is 1)</param>
    public void InstantiateStructure(Vector3Int position, GameObject structurePrefab, CellType type, int width = 1, int height = 1)
    {
        // The structure has to rotate 180 degrees on the Y axis to face the camera
        GameObject newStructure = Instantiate(structurePrefab, position, Quaternion.Euler(0, 180, 0));

        // Parent the structure to the buildings parent for organization
        // This is also important for the rotation mehecanic to work properly
        newStructure.transform.parent = buildingsParent; 
        AudioManager.instance.PlayPlacementSound(); // Plays a sound for feedback

        // Loop through the width and height of the structure
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                // Calculate the new position and set the type of the cell
                var newPosition = position + new Vector3Int(x, 0, z);
                StructureManager.instance.placementGrid[newPosition.x, newPosition.z] = type;
                DestroyNatureAt(newPosition); // Destroy any nature object at the position
            }
        } 
    }

    /// <summary>
    /// Destroys the nature objects at the given position
    /// </summary>
    /// <param name="position">The position to destroy the nature</param>
    void DestroyNatureAt(Vector3Int position)
    {
        // Check if there is any nature object around the given position
        RaycastHit[] hits = Physics.BoxCastAll(position + new Vector3(0, 0.5f, 0), new Vector3(0.5f, 0.5f, 0.5f), transform.up, Quaternion.identity, 1f, 1 << LayerMask.NameToLayer("Nature"));

        // Loop through each nature object and apply the explosion effect for each one
        foreach (var item in hits)
        {
            // Instantiate the explosion fragments in various locations
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

            // Apply the explosion force to the nature fragments with the center in the nature object
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
