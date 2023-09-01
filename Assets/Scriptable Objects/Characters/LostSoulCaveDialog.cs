using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LostSoulCaveDialog : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject panelObject;
    public float panelTextOffset = 10f;

    private string[] sentences = {
    "PLAYER:  Hello there. I've noticed you're lost and in need of help. What's troubling you?\n",
    "LOST SOUL:  It's the spiders, you see. I'm so scared I can't get past them.\n",
    "PLAYER :  Spiders? I understand that fear can be paralyzing, but don't worry.\n We'll face this fear together. Can you tell me more about it?\n",
    "LOST SOUL:  My bones, my mortal remains, are in this very cave, and I remember now.\n I died here, tangled in webs, and they drained my life away.\n",
    "PLAYER:  I see. We're going to find your bones and ensure you find peace.\n",
    "LOST SOUL:  Thank you! Your words give me hope.\n  I just can't bring myself to go back there.\n",
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

            // trigger DialogStarted() in RayCast.cs
            GameObject.Find("Player").GetComponent<RayCast>().DialogStarted();
            // Increase Box Collider size to make it easier to click on the panel
            gameObject.GetComponent<BoxCollider>().size = new Vector3(200f, 200f, 60f);
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

                // trigger DialogEnded() in RayCast.cs
                GameObject.Find("Player").GetComponent<RayCast>().DialogEnded();
                // Reset Box Collider size
                gameObject.GetComponent<BoxCollider>().size = new Vector3(6f, 10f, 16f);
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
        Debug.Log("tekst");
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
