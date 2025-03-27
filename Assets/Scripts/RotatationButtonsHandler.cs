using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Defines the type of rotation
/// </summary>
public enum RotationType
{
    Left,
    Right,
    Reset
}

/// <summary>
/// Handles the rotation of an object based on the button pressed
/// </summary>
public class RotatationButtonsHandler : MonoBehaviour, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
{
    // Tracks if the button is pressed
    bool _isPressed;

    [Header("Settings")]
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] RotationType rotationType;
    [SerializeField] Transform objectToRotate;

    public void OnUpdateSelected(BaseEventData eventData)
    {
        // If the button is pressed, rotate the object
        if (_isPressed)
        {
            // Rotate the object based on the rotation type
            if (rotationType == RotationType.Left)
                objectToRotate.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            else if (rotationType == RotationType.Right)
                objectToRotate.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
            else if (rotationType == RotationType.Reset)
                objectToRotate.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
    }
}
