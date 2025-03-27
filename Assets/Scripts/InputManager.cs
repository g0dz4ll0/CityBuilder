using UnityEngine;

/// <summary>
/// Manages the game input
/// </summary>
public class InputManager : MonoBehaviour
{
    // The vector that will be used to move the camera
    Vector2 _cameraMovementVector;

    [SerializeField] CameraMovement cameraMovement;

    // Singleton pattern to make sure there is only one instance of the InputManager
    public static InputManager instance;

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

    void Update()
    {
        // Compute the camera movement vector based on the input
        CheckInput();
        cameraMovement.MoveCamera(new Vector3(_cameraMovementVector.x, 0, _cameraMovementVector.y)); // Move the camera just on the X and Z axis
    }

    /// <summary>
    /// Changes the vector to move the camera based on the horizontal and vertical axis
    /// </summary>
    void CheckInput()
    {
        _cameraMovementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
