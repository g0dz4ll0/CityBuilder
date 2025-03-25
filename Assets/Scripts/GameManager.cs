using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CameraMovement cameraMovement;

    [SerializeField] InputManager inputManager;

    void Start()
    {
        
    }

    void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x, 0, inputManager.CameraMovementVector.y));
    }
}
