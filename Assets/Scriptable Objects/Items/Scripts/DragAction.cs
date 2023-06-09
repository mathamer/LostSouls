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
            // also play the sound of the item that got dragged into
            eventData.pointerEnter.GetComponent<DragAction>().combineSound.PlayOneShot(eventData.pointerEnter.GetComponent<DragAction>().combineClip);


            if (combinable.combinableWithNames.Contains(gameObject.GetComponent<Combinable>().inputItem))
            {
                if (eventData.pointerEnter.GetComponent<Combinable>().requiredAmount > 1)
                {
                    Debug.Log("Dragged into multi amount item", this);
                    Player.instance.inventory.AddItem(gameObject.GetComponent<Combinable>().result, 1);
                    Player.instance.inventory.RemoveItem(gameObject.GetComponent<Combinable>().inputItem, 1);
                    Destroy(gameObject);
                }
                else if (gameObject.GetComponent<Combinable>().requiredAmount > 1)
                {
                    Debug.Log("Multi amount item dragged into item");
                    Player.instance.inventory.AddItem(eventData.pointerEnter.GetComponent<Combinable>().result, 1);
                    Player.instance.inventory.RemoveItem(eventData.pointerEnter.GetComponent<Combinable>().inputItem, 1);
                    Destroy(eventData.pointerEnter);
                }
                else
                {
                    Debug.Log("Dragged item into item");

                    if (eventData.pointerEnter.GetComponent<Combinable>().requiredAmount == 0)
                    {
                        Player.instance.inventory.AddItem(gameObject.GetComponent<Combinable>().result, 1);
                    }
                    else
                    {
                        Player.instance.inventory.AddItem(gameObject.GetComponent<Combinable>().result, 2);
                    }

                    // If the dragged item is a BloodyDagger, then keep the dagger and remove the other item
                    if (eventData.pointerEnter.GetComponent<Combinable>().inputItem == "BloodyDagger")
                    {
                        Player.instance.inventory.RemoveItem(gameObject.GetComponent<Combinable>().inputItem, 1);
                        Destroy(gameObject);
                    }
                    else if (gameObject.GetComponent<Combinable>().inputItem == "BloodyDagger")
                    {
                        Player.instance.inventory.RemoveItem(eventData.pointerEnter.GetComponent<Combinable>().inputItem, 1);
                        Destroy(eventData.pointerEnter);
                    }
                    else
                    {
                        Player.instance.inventory.RemoveItem(eventData.pointerEnter.GetComponent<Combinable>().inputItem, 1);
                        Player.instance.inventory.RemoveItem(gameObject.GetComponent<Combinable>().inputItem, 1);

                        Destroy(gameObject);
                        Destroy(eventData.pointerEnter);
                    }
                }
            }
            else
            {
                rectTransform.anchoredPosition = startPosition;
                Debug.Log("Not in the list of combinable items");

                // Skip TriggerSentences if the script is not on the Player gameobject
                if (Player.instance.GetComponent<PlayerFeedback>())
                {
                    // Trigger the TriggerSentences inside PlayerFeedback.cs that is on the Player gameobject
                    Player.instance.GetComponent<PlayerFeedback>().TriggerSentences("Not combinable");
                }
                else
                {
                    Debug.Log("PlayerFeedback.cs script not found on Player gameobject");
                }
            }
        }
        else
        {
            rectTransform.anchoredPosition = startPosition;
            Debug.Log("Not combinable");


            // Skip TriggerSentences if the script is not on the Player gameobject
            if (Player.instance.GetComponent<PlayerFeedback>())
            {
                // Trigger the TriggerSentences inside PlayerFeedback.cs that is on the Player gameobject
                Player.instance.GetComponent<PlayerFeedback>().TriggerSentences("Not combinable");
            }
            else
            {
                Debug.Log("PlayerFeedback.cs script not found on Player gameobject");
            }
        }

        // This is for combing items outside of the inventory on the scene
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
        {
            if (gameObject.GetComponent<Audio>())
            {
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
                    // Destroy(hit.collider.gameObject);

                    // trigger function in all gameobjects with GateDoor.cs script
                    GateDoor[] gateDoors = FindObjectsOfType<GateDoor>();
                    foreach (GateDoor gateDoor in gateDoors)
                    {
                        gateDoor.Interact();
                    }
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
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
}