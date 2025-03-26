using System;
using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragHandler : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask groundMask;
    [SerializeField] GameObject structurePrefab;
    [SerializeField] StructureManager structureManager;
    [SerializeField] RectTransform uiImage;
    [SerializeField] RectTransform canvas;

    Vector2 _originalLocalPointerPosition;
    Vector3 _originalPanelLocalPosition;
    Vector2 _originalPosition;

    void Start()
    {
        _originalPosition = uiImage.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalPanelLocalPosition = uiImage.localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, eventData.position, eventData.pressEventCamera, out _originalLocalPointerPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            Vector3 offsetToOriginal = localPointerPosition - _originalLocalPointerPosition;

            uiImage.localPosition = _originalPanelLocalPosition + offsetToOriginal;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(MoveUIElement(uiImage, _originalPosition, 0.5f));

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
            Debug.Log("Position: " + positionInt);
            structureManager.PlaceStructure(positionInt, structurePrefab);
        }
        else
        {
            return;
        }   
    }

    IEnumerator MoveUIElement(RectTransform r, Vector2 targetPosition, float duration = 0.1f)
    {
        float elapsedTime = 0;
        Vector2 startPosition = r.localPosition;
        while (elapsedTime < duration)
        {
            r.localPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        r.localPosition = targetPosition;
    }
}
