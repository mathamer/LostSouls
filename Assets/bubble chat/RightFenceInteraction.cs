using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RightFenceInteraction : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject panelObject;
    public float panelTextOffset = 10f; // Offset to add to the text width for panel width

    private string[] sentences = {
        "There is a person sitting on a bench",
        "Hello . . . can you hear me?",
        " . . . ..",
        "Can you help me?",
        " . . . ..",
        "She does not hear me",
        "I have to find a way to get to her"
    };

    private float typingSpeed = 0.1f;
    private int currentSentenceIndex = -1;
    private bool isDisplayingText = false;
    private bool isInstantDisplayRequested = false;

    void Start()
    {
        messageText.gameObject.SetActive(false);
        panelObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panelObject.SetActive(true);
            ResizePanel();
            messageText.gameObject.SetActive(true);
            ShowNextSentence();
        }
    }

    void OnMouseDown()
    {
        if (messageText.gameObject.activeSelf)
        {
            if (isDisplayingText)
            {
                isInstantDisplayRequested = true;
            }
            else
            {
                ShowNextSentence();
            }
        }
    }

    void ShowNextSentence()
    {
        currentSentenceIndex++;
        if (currentSentenceIndex < sentences.Length)
        {
            messageText.text = "";
            StartDisplayingText();
        }
        else
        {
            panelObject.SetActive(false);
            messageText.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void StartDisplayingText()
    {
        isDisplayingText = true;
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        string sentence = sentences[currentSentenceIndex];
        int currentCharacterIndex = 0;

        while (currentCharacterIndex < sentence.Length)
        {
            if (isInstantDisplayRequested)
            {
                messageText.text = sentence;
                currentCharacterIndex = sentence.Length;
                isInstantDisplayRequested = false;
            }
            else
            {
                messageText.text += sentence[currentCharacterIndex];
                currentCharacterIndex++;
            }

            ResizePanel();
            yield return new WaitForSeconds(typingSpeed);
        }

        isDisplayingText = false;
        ResizePanel();
    }

    void ResizePanel()
    {
        float textWidth = messageText.preferredWidth;
        Vector2 panelSize = new Vector2(textWidth + panelTextOffset, panelObject.GetComponent<RectTransform>().sizeDelta.y);
        panelObject.GetComponent<RectTransform>().sizeDelta = panelSize;
    }
}
