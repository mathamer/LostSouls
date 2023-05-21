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

        // This is for combining items inside the inventory
        if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<Combinable>() && eventData.pointerEnter.GetComponent<Combinable>().combinableWithNames.Contains(gameObject.GetComponent<Combinable>().inputItem))
        {
            Debug.Log("Combine");

            if (eventData.pointerEnter.GetComponent<Combinable>().result == null)
            {
                Debug.Log("Dragged into xylophone");
                AudioSource.PlayClipAtPoint(gameObject.GetComponent<Combinable>().combineSound, transform.position);
                Player.instance.inventory.AddItem(gameObject.GetComponent<Combinable>().result, 1);
                Player.instance.inventory.RemoveItem(gameObject.GetComponent<Combinable>().inputItem, 1);
                Destroy(gameObject);
            } else if (gameObject.GetComponent<Combinable>().result == null)
            {
                Debug.Log("Xylophone dragged into item");  
                AudioSource.PlayClipAtPoint(gameObject.GetComponent<Combinable>().combineSound, transform.position);
                Player.instance.inventory.AddItem(eventData.pointerEnter.GetComponent<Combinable>().result, 1);
                Player.instance.inventory.RemoveItem(eventData.pointerEnter.GetComponent<Combinable>().inputItem, 1);
                Destroy(eventData.pointerEnter);
            } else
            {
                Debug.Log("Dragged item into item");
                AudioSource.PlayClipAtPoint(gameObject.GetComponent<Combinable>().combineSound, transform.position);
                Player.instance.inventory.AddItem(eventData.pointerEnter.GetComponent<Combinable>().result, 2);
                Player.instance.inventory.RemoveItem(eventData.pointerEnter.GetComponent<Combinable>().inputItem, 1);
                Player.instance.inventory.RemoveItem(gameObject.GetComponent<Combinable>().inputItem, 1);
                Destroy(gameObject);
                Destroy(eventData.pointerEnter);
            }
        } else {
            rectTransform.anchoredPosition = startPosition;
            Debug.Log("Nope");
        }

        // This is for combing items outside of the inventory
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
        {
            Debug.Log("Raycast hit");
            if (hit.collider.gameObject.GetComponent<Combinable>() && hit.collider.gameObject.GetComponent<Combinable>().combinableWithNames.Contains(gameObject.GetComponent<Combinable>().inputItem))
            {
                Debug.Log("Combine");
                if (hit.collider.gameObject == Player.instance.gameObject)
                {
                    Debug.Log("Dragged into player");
                    AudioSource.PlayClipAtPoint(gameObject.GetComponent<Combinable>().combineSound, transform.position);
                    Player.instance.inventory.AddItem(hit.collider.gameObject.GetComponent<Combinable>().result, 1);
                    Player.instance.inventory.RemoveItem(gameObject.GetComponent<Combinable>().inputItem, 1);
                    Destroy(gameObject);
                }
                // check if dragged into gate
                else if (hit.collider.gameObject)
                {
                    Debug.Log("Dragged into gate");
                    AudioSource.PlayClipAtPoint(gameObject.GetComponent<Combinable>().combineSound, transform.position);
                    Player.instance.inventory.RemoveItem(gameObject.GetComponent<Combinable>().inputItem, 1);
                    Destroy(gameObject);
                    Destroy(hit.collider.gameObject);
                }   
            }
            else
            {
                rectTransform.anchoredPosition = startPosition;
                Debug.Log("Nope");
            }
        }
        else
        {
            rectTransform.anchoredPosition = startPosition;
            Debug.Log("Nope");
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
