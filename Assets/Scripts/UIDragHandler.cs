using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles the dragging of the UI element and places the structure in the world
/// </summary>
public class UIDragHandler : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    [Header("References")]
    [SerializeField] Camera mainCamera;
    [Tooltip("Prefab to instantiate when the UI element is dropped in the World")]
    [SerializeField] GameObject structurePrefab;
    [Tooltip("UI Image to drag around")]
    [SerializeField] RectTransform uiImage;
    [Tooltip("Canvas to calculate the position of the UI element")]
    [SerializeField] RectTransform canvas;
    [SerializeField] StructureManager structureManager;

    [Header("Settings")]
    [Tooltip("The layer mask to detect if the raycast is pointing to the ground")]
    [SerializeField] LayerMask groundMask;
    [Tooltip("Check if the structure is a big structure or not")]
    [SerializeField] bool isBigStructure = false;

    // Variables to store the original position of the UI element
    Vector2 _originalLocalPointerPosition;
    Vector3 _originalPanelLocalPosition;
    Vector2 _originalPosition;

    void Start()
    {
        // Save the original position of the UI element for later use
        _originalPosition = uiImage.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Calculate the position of the UI element for later reference for the dragging animation
        _originalPanelLocalPosition = uiImage.localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, eventData.position, eventData.pressEventCamera, out _originalLocalPointerPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Calculate the position of the UI element based on the pointer position
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            Vector3 offsetToOriginal = localPointerPosition - _originalLocalPointerPosition;

            uiImage.localPosition = _originalPanelLocalPosition + offsetToOriginal;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Reset the position of the UI element to the original position with a smooth animation
        StartCoroutine(MoveUIElement(uiImage, _originalPosition, 0.5f));

        // Check if the raycast hits the ground and place the structure in the hit position
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
            Debug.Log("Position: " + positionInt);

            // Verify if it is a big structure or not and apply the correct placement method
            if (!isBigStructure)
                structureManager.PlaceStructure(positionInt, structurePrefab);
            else
                structureManager.PlaceBigStructure(positionInt, structurePrefab);
        }
        else // If the raycast doesn't hit the ground, do not place the structure
        {
            return;
        }   
    }

    /// <summary>
    /// Moves the UI element to the target position with a smooth animation
    /// </summary>
    /// <param name="image">The image to move around</param>
    /// <param name="targetPosition">The position to move the image to</param>
    /// <param name="duration">The duration of the transition</param>
    IEnumerator MoveUIElement(RectTransform image, Vector2 targetPosition, float duration = 0.1f)
    {
        // The elapsed time of the transition to keep track of the time
        float elapsedTime = 0;
        Vector2 startPosition = image.localPosition; // The current position of the image to start the animation

        // While the elapsed time is less than the duration, move the image to the target position
        while (elapsedTime < duration)
        {
            // Lerp the position for a smooth transition
            image.localPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration); 
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // If the duration is over, set the position to the target position so it doesn't overshoot
        image.localPosition = targetPosition; 
    }
}
