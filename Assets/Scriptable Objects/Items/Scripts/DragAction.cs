using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAction : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;

    private RectTransform rectTransform;
    private Vector2 startPosition;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvas.overrideSorting = true;
        canvas.sortingOrder = 99;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvas.overrideSorting = false;
        canvas.sortingOrder = 0;

        // Check if the dragged item is released over a drag interactable object
        if (eventData.pointerEnter == null || !eventData.pointerEnter.GetComponent<DragInteractable>())
        {
            // If not, return the dragged item to its original position
            rectTransform.anchoredPosition = startPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out position))
        {
            rectTransform.anchoredPosition = canvas.transform.TransformPoint(position);
        }
    }
}
