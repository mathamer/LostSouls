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
            if (combineClip != null)
            {
                combineSound.PlayOneShot(combineClip);
            }
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
                    if (Player.instance.GetComponent<PlayerFeedback>())
                    {
                        // Trigger the TriggerSentences inside PlayerFeedback.cs that is on the Player gameobject
                        Player.instance.GetComponent<PlayerFeedback>().TriggerSentences("hurt");
                    }
                    else
                    {
                        Debug.Log("PlayerFeedback.cs script not found on Player gameobject");
                    }
                }
                // check if dragged into gate
                else if (hit.collider.gameObject)
                {
                    // if dagger is dragged onto Dog, remove the Dog if LeafPlace is enabled
                    if (hit.collider.gameObject.GetComponent<Combinable>().inputItem == "Dog" && States.instance.leafsPlaced)
                    {
                        States.instance.dogRopeCut = true;
                        hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        hit.collider.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                        hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                        hit.collider.gameObject.GetComponent<Combinable>().combineSound.PlayOneShot(hit.collider.gameObject.GetComponent<Combinable>().combineClip);
                        return;
                    }
                    else if (hit.collider.gameObject.GetComponent<Combinable>().inputItem == "Dog" && !States.instance.leafsPlaced)
                    {
                        Debug.Log("Leafs not placed");
                        return;
                    }

                    Debug.Log("Dragged into object with combinable script");
                    // removes all instances of the item instead of just one
                    Player.instance.inventory.RemoveItem(gameObject.GetComponent<Combinable>().inputItem, Player.instance.inventory.Container[Player.instance.inventory.Container.FindIndex(i => i.item.name == gameObject.GetComponent<Combinable>().inputItem)].amount);
                    Destroy(gameObject);

                    // Add the item to the inventory if result is not null
                    if (hit.collider.gameObject.GetComponent<Combinable>().result != null)
                    {
                        Player.instance.inventory.AddItem(hit.collider.gameObject.GetComponent<Combinable>().result, 1);
                    }


                    // if dragged onto LeafPlace, enable the LeafPlace child
                    if (hit.collider.gameObject.GetComponent<Combinable>().inputItem == "LeafPlace")
                    {
                        States.instance.leafsPlaced = true;
                        hit.collider.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                        hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                        hit.collider.gameObject.GetComponent<Combinable>().combineSound.PlayOneShot(hit.collider.gameObject.GetComponent<Combinable>().combineClip);
                    }

                    // if dragged onto Hangign Tree, set ballThrown to true
                    if (hit.collider.gameObject.GetComponent<Combinable>().inputItem == "Hangign Tree")
                    {
                        States.instance.ballThrown = true;
                        hit.collider.gameObject.GetComponent<Combinable>().combineSound.PlayOneShot(hit.collider.gameObject.GetComponent<Combinable>().combineClip);
                    }

                    if (hit.collider.gameObject.GetComponent<Combinable>().inputItem == "RoomSoul")
                    {
                        States.instance.maketaOnMonster = true;
                    }

                    if (hit.collider.gameObject.GetComponent<Combinable>().inputItem == "NEW - LostSoulTheater" && !States.instance.dressGiven)
                    {
                        States.instance.dressGiven = true;
                        return;
                    }
                    if (hit.collider.gameObject.GetComponent<Combinable>().inputItem == "NEW - LostSoulTheater" && !States.instance.musicBoxGiven)
                    {
                        States.instance.musicBoxGiven = true;
                        return;
                    }

                    if (hit.collider.gameObject.GetComponent<Combinable>().inputItem == "Spider")
                    {
                        States.instance.crowOnSpider = true;
                    }

                    if (hit.collider.gameObject.GetComponent<Combinable>().inputItem == "LostSoulCave")
                    {
                        Debug.Log("GIRLLLLLL");
                        States.instance.bonesOnGirl = true;
                    }

                    if (hit.collider.gameObject.GetComponent<Combinable>().inputItem == "NEW - LostSoul1")
                    {
                        States.instance.collarGiven = true;
                    }

                    if (hit.collider.gameObject.GetComponent<Combinable>().inputItem == "Theater" && !States.instance.firstKeyUsed)
                    {
                        States.instance.firstKeyUsed = true;
                        return;
                    }
                    if (hit.collider.gameObject.GetComponent<Combinable>().inputItem == "Theater" && !States.instance.secondKeyUsed)
                    {
                        States.instance.secondKeyUsed = true;
                        return;
                    }
                    if (hit.collider.gameObject.GetComponent<Combinable>().inputItem == "Theater" && !States.instance.thirdKeyUsed)
                    {
                        States.instance.thirdKeyUsed = true;
                        return;
                    }


                    // // trigger function in all gameobjects with GateDoor.cs script if dragged into GateDoor
                    // if (hit.collider.gameObject.GetComponent<Combinable>().inputItem == "GateDoor")
                    // {
                    //     GateDoor[] gateDoors = FindObjectsOfType<GateDoor>();
                    //     foreach (GateDoor gateDoor in gateDoors)
                    //     {
                    //         gateDoor.Interact();
                    //     }
                    // }
                }
                else
                {
                    rectTransform.anchoredPosition = startPosition;
                    Debug.Log("Raycast hit combinable item but it not in the list of combinable items");

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