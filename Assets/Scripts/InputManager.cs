using System;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    Vector2 cameraMovementVector;

    [SerializeField] Camera mainCamera;

    [SerializeField] LayerMask groundMask;

    [SerializeField] GameObject structurePrefab;

    public Vector2 CameraMovementVector
    {
        get { return cameraMovementVector; }
    }

    void Start()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //TODO: Cache initial UI image location for dragging
    }

    public void OnDrag(PointerEventData eventData)
    {
        //TODO: Move UI image with mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //TODO: Implement coroutine to move UI image back to initial location

        Vector3Int? worldPosition = RaycastGround();
        PlaceObject(worldPosition);
    }

    void PlaceObject(Vector3Int? worldPosition)
    {
        if (structurePrefab == null)
        {
            Debug.Log("No prefab to instantiate!");
            return;
        }

        GameObject obj = Instantiate(structurePrefab, worldPosition.Value, Quaternion.identity);
    }

    void Update()
    {
        CheckArrowInput();
    }

    Vector3Int? RaycastGround()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
            return positionInt;
        }
        return null;
    }

    void CheckArrowInput()
    {
        cameraMovementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
