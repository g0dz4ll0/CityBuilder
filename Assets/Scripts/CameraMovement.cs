using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Camera gameCamera;
    [SerializeField] float cameraMovementSpeed = 5;

    void Start()
    {
        gameCamera = GetComponent<Camera>();
    }

    public void MoveCamera(Vector3 inputVector)
    {
        var movementVector = Quaternion.Euler(0, 30, 0) * inputVector;
        gameCamera.transform.position += movementVector * Time.deltaTime * cameraMovementSpeed;
    }
}
