using UnityEngine;

public class GameManager : MonoBehaviour
{
    Vector2 cameraMovementVector;

    [SerializeField] CameraMovement cameraMovement;
    [SerializeField] UIDragHandler inputManager;

    void Start()
    {
        
    }

    void Update()
    {
        CheckArrowInput();
        cameraMovement.MoveCamera(new Vector3(cameraMovementVector.x, 0, cameraMovementVector.y));
    }

    void CheckArrowInput()
    {
        cameraMovementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
