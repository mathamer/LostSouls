using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAction : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;

    private RectTransform rectTransform;
    private Vector2 startPosition;
    private Image image;
    public AudioSource combineSound;
    public AudioClip combineClip;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {   
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvas.overrideSorting = true;
        canvas.sortingOrder = 99;
        image.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Combinable combinable = eventData.pointerEnter?.GetComponent<Combinable>();
        canvas.overrideSorting = false;
        canvas.sortingOrder = 0;
        image.raycastTarget = true;

        // This is for combining items inside the inventory
        if (combinable != null && gameObject.GetComponent<Combinable>() != null)
        {
            Debug.Log("Combine");
            combineSound.PlayOneShot(combineClip);

            if (combinable.combinableWithNames.Contains(gameObject.GetComponent<Combinable>().inputItem))
            {
                if (eventData.pointerEnter.GetComponent<Combinable>().requiredAmount > 1)
                {
                    Debug.Log("Dragged into multi amount item", this);
                    Player.instance.inventory.AddItem(gameObject.GetComponent<Combinable>().result, 1);
                    Player.instance.inventory.RemoveItem(gameObject.GetComponent<Combinable>().inputItem, 1);
                    Destroy(gameObject);
                } else if (gameObject.GetComponent<Combinable>().requiredAmount > 1)
                {
                    Debug.Log("Multi amount item dragged into item");  
                    Player.instance.inventory.AddItem(eventData.pointerEnter.GetComponent<Combinable>().result, 1);
                    Player.instance.inventory.RemoveItem(eventData.pointerEnter.GetComponent<Combinable>().inputItem, 1);
                    Destroy(eventData.pointerEnter);
                } else
                {
                        Debug.Log("Dragged item into item");
                        if (eventData.pointerEnter.GetComponent<Combinable>().requiredAmount == 0)
                        {
                            Player.instance.inventory.AddItem(gameObject.GetComponent<Combinable>().result, 1);
                        } else
                        {
                            Player.instance.inventory.AddItem(gameObject.GetComponent<Combinable>().result, 2);
                        }
                        Player.instance.inventory.RemoveItem(eventData.pointerEnter.GetComponent<Combinable>().inputItem, 1);
                        Player.instance.inventory.RemoveItem(gameObject.GetComponent<Combinable>().inputItem, 1);

                        //TODO hide UI element but leave object until sound is played
                        //image.enabled = false;
                        //Destroy(gameObject,0.7f);
                        
                        Destroy(gameObject);
                        Destroy(eventData.pointerEnter);     
                }
            }
            else
            {
                rectTransform.anchoredPosition = startPosition;
                Debug.Log("Not in the list of combinable items");
            }
        } else {
            rectTransform.anchoredPosition = startPosition;
            Debug.Log("Not combinable");
        }   

        // This is for combing items outside of the inventory on the scene
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
        {
            if (gameObject.GetComponent<Audio>()) {
                gameObject.GetComponent<Audio>().playAudio(hit.collider.gameObject == Player.instance.gameObject);
            }
         
            if (hit.collider.gameObject.GetComponent<Combinable>() && hit.collider.gameObject.GetComponent<Combinable>().combinableWithNames.Contains(gameObject.GetComponent<Combinable>().inputItem))
            {
                Debug.Log("Raycast hit combinable item");
                if (hit.collider.gameObject == Player.instance.gameObject)
                {
                    Debug.Log("Dragged into player");
                    Player.instance.inventory.AddItem(hit.collider.gameObject.GetComponent<Combinable>().result, 1);
                    Player.instance.inventory.RemoveItem(gameObject.GetComponent<Combinable>().inputItem, 1);
                    Destroy(gameObject);
                }
                // check if dragged into gate
                else if (hit.collider.gameObject)
                {
                    Debug.Log("Dragged into object with combinable script");
                    Player.instance.inventory.RemoveItem(gameObject.GetComponent<Combinable>().inputItem, 1);
                    Destroy(gameObject);
                    Destroy(hit.collider.gameObject);
                }  
                else
                {
                    rectTransform.anchoredPosition = startPosition;
                    Debug.Log("Raycast hit combinable item but it not in the list of combinable items");
                } 
            }
            else
            {
                rectTransform.anchoredPosition = startPosition;
            }
        }
        else
        {
            rectTransform.anchoredPosition = startPosition;
            Debug.Log("Raycast didn't hit");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out position))
        {
            // // Set the anchored position of the RectTransform relative to the canvas
            // rectTransform.anchoredPosition = position;

            // // Adjust the anchored position to keep the item centered
            // rectTransform.anchoredPosition -= rectTransform.sizeDelta / 2f;
            
            // // Convert the anchored position to world space
            // Vector3 worldPosition = canvas.transform.TransformPoint(rectTransform.anchoredPosition);
            
            // // Set the position of the dragged item in world space
            // rectTransform.position = worldPosition;

            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
}