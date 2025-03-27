using UnityEngine;

/// <summary>
/// Handles the movement of the camera
/// </summary>
public class CameraMovement : MonoBehaviour
{
    Camera _gameCamera;
    [SerializeField] float cameraMovementSpeed = 5;

    void Start()
    {
        _gameCamera = GetComponent<Camera>();
    }

    /// <summary>
    /// Moves the camera based on the input vector
    /// </summary>
    /// <param name="inputVector">The players input</param>
    public void MoveCamera(Vector3 inputVector)
    {
        var movementVector = inputVector;
        _gameCamera.transform.position += movementVector * Time.deltaTime * cameraMovementSpeed;
    }
}
