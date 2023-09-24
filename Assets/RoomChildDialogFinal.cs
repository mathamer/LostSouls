using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomChildDialogFinal : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject panelObject;
    //private bool isDialogFinished = false; 

    [SerializeField]
    private string[] sentences = {
        "MAX: WHAT HAPPENED? WHERE AM I?\n",
        "PLAYER: YOU'RE IN A PLACE UNLIKE ANY OTHER, WHERE ENDLESS PLAY AWAITS YOU.\n",
        "MAX: ARE MY PARENTS HERE?\n",
        "PLAYER: THEY'LL JOIN YOU IN TIME. MEANWHILE, YOU'LL HAVE MANY FRIENDS TO KEEP YOU COMPANY.\n",
    };

    private float typingSpeed = 0.1f;
    private int currentSentenceIndex = -1;
    private bool isDisplayingText = false;
    private bool hasDisplayedText = false;

    void Start()
    {
        panelObject.SetActive(false);
        messageText.gameObject.SetActive(false);

    }
    void Update()
    {
        if (States.instance.maketaOnMonster && !hasDisplayedText)
        {
            panelObject.SetActive(true);
            messageText.gameObject.SetActive(true);
            ShowNextSentence();
            hasDisplayedText = true;

            GameObject.Find("Player").GetComponent<RayCast>().DialogStarted();
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
            }
            else if (currentSentenceIndex < sentences.Length - 1)
            {
                ShowNextSentence();
            }
            else
            {
                //isDialogFinished = true;
                panelObject.SetActive(false);
                messageText.gameObject.SetActive(false);

                States.instance.ThirdSoulQuest = true;

                GameObject.Find("Player").GetComponent<RayCast>().DialogEnded();
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
            yield return new WaitForSeconds(typingSpeed);
        }

        isDisplayingText = false;
    }
}
