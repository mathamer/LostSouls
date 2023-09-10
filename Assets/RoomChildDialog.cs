using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomChildDialog : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject panelObject;
    public float panelTextOffset = 10f;
    public AudioSource textAudio;
    public AudioClip[] clips;
    private bool isMessageShown = false;
    private bool isFirstClick = true;

    private string[] targetSentences = {
    "PLAYER:   Hello!\n",
    "LOST SOUL:  Plane...\n",
    "LOST SOUL:  Plane...\n",
    "LOST SOUL:  Father...\n",
    "LOST SOUL:  *I guess he wants this plane model.*\n",
};

    private float typingSpeed = 0.1f;
    private int currentSentenceIndex = 0;
    private bool isDisplayingText = false;
    private bool hasDisplayedText = false;

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

        // OVDJE DRUGO AKO JE KAO NAHRANJEN
        // if (gateDoor.GetComponent<GateDoor>().isOpen)
        // {
        //     DisableBoxCollider();
        // }

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
            enabled = false;
            // DisableBoxCollider();
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
            textAudio.Play();
            yield return new WaitForSeconds(textAudio.clip.length);
        }
    }
}
