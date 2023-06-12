using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DogDialog : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject panelObject;
    public float panelTextOffset = 10f; // Offset to add to the text width for panel width

    private string[] sentences = {
    " PLAYER: (WHISPERING) WHAT IS THIS?\n ",
    " WHO COULD HAVE INFLICTED SUCH CRUELTY UPON THIS POOR DOG?\n",
    " ITS LIFELESS BODY HANGS FROM A BRANCH,\n",
    " A CHILLING SIGHT IN THE HEART OF THE FOREST.\n",
    " AS I EXAMINE THE SCENE, A HAUNTING REALIZATION STARTS TO FORM WITHIN ME.\n",
    " THE DOG MUST HAVE BEEN SEEKING SOLACE\n",
    " IN THE SHADE BENEATH THE TREE, COMFORTED BY THE JOYFUL LAUGHTER OF A BOY\n",
    " PLAYING WITH HIS FRIENDS NEARBY.\n",
    " BUT AS THE LAUGHTER FADED AWAY,\n",
    " REPLACED BY SILENCE, UNEASE GRIPPED THE DOG'S HEART.\n",
    " IT YEARNED TO CATCH ANOTHER GLIMPSE OF ITS BELOVED COMPANION,\n",
    " THE SOURCE OF ITS HAPPINESS.\n",
    " IN A DESPERATE ATTEMPT TO SEE,\n",
    " IT LEAPED ONTO THE TREE BRANCH,\n",
    " ITS FATE SEALED IN THAT MOMENT.\n",
    " BOUND TO THE TREE, IT TWISTED AND ENTANGLED ITSELF,\n",
    " ITS LIFE EXTINGUISHED, FOREVER SUSPENDED IN SORROW.\n"
};

    private float typingSpeed = 0.1f;
    private int currentSentenceIndex = -1;
    private bool isDisplayingText = false;
    private bool hasDisplayedText = false;

    void Start()
    {
        messageText.gameObject.SetActive(false);
        panelObject.SetActive(false);
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
        if (messageText.gameObject.activeSelf)
        {
            if (isDisplayingText)
            {
                isDisplayingText = false;
                StopAllCoroutines();
                messageText.text = sentences[currentSentenceIndex];
                ResizePanel();
            }
            else if (currentSentenceIndex < sentences.Length - 1)
            {
                ShowNextSentence();
            }
            else
            {
                panelObject.SetActive(false);
                messageText.gameObject.SetActive(false);
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
            messageText.text += sentence[currentCharacterIndex];
            currentCharacterIndex++;
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
