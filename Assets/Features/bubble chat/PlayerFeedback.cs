using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerFeedback : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject panelObject;
    public float panelTextOffset = 10f; // Offset to add to the text width for panel width

    private string[] notCombinable = {
        "That won't work",
        "I don't think that's right",
        "It doesn't fit"
    };

    private string[] hints = {
        "I should look around some more",
        "There are some items I haven't used yet",
        "I think you're missing something"
    };

    private string[] hurt = {
        "Ouch.. this better work",
    };

    private string[] sentences;

    private float typingSpeed = 0.1f;
    private int currentSentenceIndex = -1;
    private bool isDisplayingText = false;
    private bool hasDisplayedText = false;

    void Start()
    {
        messageText.gameObject.SetActive(false);
        panelObject.SetActive(false);
    }

    public void TriggerSentences(string type)
    {
        Debug.Log("TriggerSentences called " + type);

        messageText.gameObject.SetActive(true);
        panelObject.SetActive(true);


        if (type == "Not combinable")
        {
            sentences = notCombinable;
            Debug.Log("sentences set to not combinable");
        }
        if (type == "hint")
        {
            sentences = hints;
            Debug.Log("sentences set to hints");
        }
        if (type == "hurt")
        {
            sentences = hurt;
            Debug.Log("sentences set to hurt");
        }

        if (currentSentenceIndex < sentences.Length - 1)
        {
            ShowNextSentence();
        }
        else
        {
            panelObject.SetActive(false);
            messageText.gameObject.SetActive(false);
            currentSentenceIndex = -1;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasDisplayedText)
        {
            panelObject.SetActive(true);
            ResizePanel();
            messageText.gameObject.SetActive(true);
            ShowNextSentence();
            hasDisplayedText = true;
        }
    }

    void OnMouseDown()
    {
        messageText.gameObject.SetActive(true);
        panelObject.SetActive(true);

        sentences = hints;

        if (currentSentenceIndex < sentences.Length - 1)
        {
            ShowNextSentence();
        }
        else
        {
            panelObject.SetActive(false);
            messageText.gameObject.SetActive(false);
            currentSentenceIndex = -1;
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
    }

    void StartDisplayingText()
    {
        StopAllCoroutines();
        isDisplayingText = true;
        StartCoroutine(AnimateText());
    }
    IEnumerator AnimateText()
    {
        string sentence = sentences[currentSentenceIndex];
        int currentCharacterIndex = 0;

        while (currentCharacterIndex < sentence.Length)
        {
            messageText.text += sentence[currentCharacterIndex];
            currentCharacterIndex++;
            ResizePanel();
            yield return new WaitForSeconds(typingSpeed);
        }

        isDisplayingText = false;
        ResizePanel();

        //when the text is done displaying, wait for 3 seconds and then hide the text
        yield return new WaitForSeconds(2);
        messageText.text = "";
        panelObject.SetActive(false);
    }

    void ResizePanel()
    {
        float textWidth = messageText.preferredWidth;
        Vector2 panelSize = new Vector2(textWidth + panelTextOffset, panelObject.GetComponent<RectTransform>().sizeDelta.y);
        panelObject.GetComponent<RectTransform>().sizeDelta = panelSize;
    }

    // Make the panel follow the player
    void Update()
    {
        if (messageText.gameObject.activeSelf)
        {
            Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
            panelObject.transform.position = playerPosition;

            // Offset the panel so it's above the player
            panelObject.transform.position += new Vector3(0, 100, 0);
        }
    }
}
