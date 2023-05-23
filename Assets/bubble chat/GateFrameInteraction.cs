using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GateFrameInteraction : MonoBehaviour
{
    public TextMeshProUGUI messageText; // Reference to the TextMeshProUGUI component
    public GameObject panelObject; // Reference to the panel object
    public float panelTextOffset = 10f; // Offset to add to the text width for panel width


    private bool isMessageShown = false; // Flag to track if the message is currently shown
    private string[] targetSentences = new string[]
    
    {
        "The gate is locked.",
        "I need to find a way to open it.",
        "There must be a way beyond.",
    };
    private float typingSpeed = 0.1f; // The speed at which each character is typed
    private int currentSentenceIndex = 0; // The index of the current sentence being displayed
    private int currentCharacterIndex = 0; // The index of the current character being displayed

    void Start()
    {
        messageText.gameObject.SetActive(false);
        panelObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse click position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits the gate frame collider
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                if (!isMessageShown)
                {
                    // Start displaying the text message
                    messageText.text = "";
                    messageText.gameObject.SetActive(true);
                    panelObject.SetActive(true);
                    ResizePanel();
                    isMessageShown = true;
                    StartCoroutine(AnimateText());
                }
                else
                {
                    if (currentCharacterIndex < targetSentences[currentSentenceIndex].Length)
                    {
                        // Finish typing the sentence instantly
                        messageText.text = targetSentences[currentSentenceIndex];
                        currentCharacterIndex = targetSentences[currentSentenceIndex].Length;
                    }
                    else
                    {
                        currentCharacterIndex = 0;
                        currentSentenceIndex++;
                        if (currentSentenceIndex < targetSentences.Length)
                        {
                            // Display the next sentence
                            StartCoroutine(AnimateText());
                        }
                        else
                        {
                            // Close the text if all sentences are displayed
                            CloseText();
                        }
                    }
                }
            }
            else if (isMessageShown)
            {
                // Close the text when clicked anywhere on the map
                CloseText();
            }
        }
    }

    IEnumerator AnimateText()
    {
        while (currentCharacterIndex < targetSentences[currentSentenceIndex].Length)
        {
            // Display the next character
            messageText.text = targetSentences[currentSentenceIndex].Substring(0, currentCharacterIndex);
            currentCharacterIndex++;

            ResizePanel();
            yield return new WaitForSeconds(typingSpeed);
        }

         // Check if all sentences have been displayed
    if (currentSentenceIndex == targetSentences.Length - 1 && currentCharacterIndex == targetSentences[currentSentenceIndex].Length)
    {
        // Close the text and destroy the objects
        CloseText();
        //DestroyObjects();
    }
    }

    void CloseText()
    {
        // Close the text
        messageText.gameObject.SetActive(false);
        panelObject.SetActive(false);
        isMessageShown = false;
    }

    void ResizePanel()
    {
        float textWidth = messageText.preferredWidth;
        Vector2 panelSize = new Vector2(textWidth + panelTextOffset, panelObject.GetComponent<RectTransform>().sizeDelta.y);
        panelObject.GetComponent<RectTransform>().sizeDelta = panelSize;
    }
}
