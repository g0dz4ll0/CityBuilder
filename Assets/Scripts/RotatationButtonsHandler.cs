using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public enum RotationType
{
    Left,
    Right,
    Reset
}

public class RotatationButtonsHandler : MonoBehaviour, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
{
    bool _isPressed;
    [SerializeField] float _rotationSpeed = 10f;
    [SerializeField] RotationType rotationType;

    [SerializeField] Transform objectToRotate;

    public void OnUpdateSelected(BaseEventData eventData)
    {
        if (_isPressed)
        {
            if (rotationType == RotationType.Left)
                objectToRotate.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
            else if (rotationType == RotationType.Right)
                objectToRotate.Rotate(Vector3.forward * -_rotationSpeed * Time.deltaTime);
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
