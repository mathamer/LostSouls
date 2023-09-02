using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GateFrameInteraction : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject panelObject;
    public float panelTextOffset = 10f;
    public AudioSource textAudio;
    public AudioClip[] clips;
    public GameObject gateDoor;

    private bool isMessageShown = false;
    private string[] targetSentences = {
        "The gate is locked.",
        "I need to find a way to open it.",
        "There must be a way beyond."
    };
    private float typingSpeed = 0.1f;
    private int currentSentenceIndex = 0;
    private bool isDisplayingText = false;
    private bool isFirstClick = true;

    void Start()
    {
        messageText.gameObject.SetActive(false);
        panelObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                if (!isMessageShown)
                {
                    messageText.text = "";
                    messageText.gameObject.SetActive(true);
                    panelObject.SetActive(true);
                    ResizePanel();
                    isMessageShown = true;
                    StartCoroutine(AnimateText(targetSentences[currentSentenceIndex]));
                    isFirstClick = true;
                }
                else if (isDisplayingText)
                {
                    if (isFirstClick)
                    {
                        isDisplayingText = false;
                        StopAllCoroutines();
                        messageText.text = targetSentences[currentSentenceIndex];
                        ResizePanel();
                        isFirstClick = false;
                    }
                    else
                    {
                        messageText.text = targetSentences[currentSentenceIndex];
                        ResizePanel();
                        isFirstClick = true;
                    }
                }
                else if (currentSentenceIndex < targetSentences.Length - 1)
                {
                    currentSentenceIndex++;
                    messageText.text = "";
                    StartCoroutine(AnimateText(targetSentences[currentSentenceIndex]));
                    isFirstClick = true;
                }
                else
                {
                    CloseText();
                }
            }
            else if (isMessageShown)
            {
                CloseText();
            }
        }
        // if gate is open then disable box collider
        if (States.instance.gateOpen)
        {
            DisableBoxCollider();
        }

    }

    IEnumerator AnimateText(string sentence)
    {
        isDisplayingText = true;
        int currentCharacterIndex = 0;
        StartCoroutine(PlayRandomSoundClip());

        while (currentCharacterIndex < sentence.Length)
        {
            if (isFirstClick)
            {
                messageText.text += sentence[currentCharacterIndex];
                currentCharacterIndex++;
                ResizePanel();
            }
            else
            {
                messageText.text = sentence;
                ResizePanel();
                break;
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        isDisplayingText = false;
        textAudio.Stop();
    }

    void CloseText()
    {
        messageText.gameObject.SetActive(false);
        panelObject.SetActive(false);
        isMessageShown = false;
        isDisplayingText = false;
        isFirstClick = true;

        if (currentSentenceIndex >= targetSentences.Length - 1)
        {
            // All sentences have been shown, stop displaying text
            enabled = false;
            DisableBoxCollider();
        }
    }

    void ResizePanel()
    {
        float textWidth = messageText.preferredWidth;
        Vector2 panelSize = new Vector2(textWidth + panelTextOffset, panelObject.GetComponent<RectTransform>().sizeDelta.y);
        panelObject.GetComponent<RectTransform>().sizeDelta = panelSize;
    }

    IEnumerator PlayRandomSoundClip()
    {
        while (isDisplayingText)
        {
            textAudio.clip = clips[Random.Range(0, clips.Length)];
            Debug.Log(textAudio.clip);
            textAudio.Play();
            yield return new WaitForSeconds(textAudio.clip.length);
        }
    }

    public void DisableBoxCollider()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}
