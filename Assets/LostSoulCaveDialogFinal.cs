using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LostSoulCaveDialogFinal : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject panelObject;
    //private bool isDialogFinished = false; 

    [SerializeField]
    private string[] sentences2 = {
      "PLAYER: I HAVE COLLECTED YOUR BONES. YOU ARE FREE NOW.\n",
    "EMILY: THANK YOU FOR YOUR KINDNESS. I THOUGHT THIS DREADFUL FEELING WOULD NEVER END.\n"
    };

    private float typingSpeed = 0.1f;
    private int currentSentenceIndex = -1;
    private bool isDisplayingText = false;
    private bool hasDisplayedText = false;
    private string[] sentences;

    void Start()
    {
        panelObject.SetActive(false);
        messageText.gameObject.SetActive(false);

    }
    void Update()
    {
        if (States.instance.bonesOnGirl && !hasDisplayedText)
        {
            GetComponent<BoxCollider>().enabled = true;
            sentences = sentences2;
            panelObject.SetActive(true);
            messageText.gameObject.SetActive(true);
            ShowNextSentence();
            hasDisplayedText = true;

            GameObject.Find("Player").GetComponent<RayCast>().DialogStarted();
            gameObject.GetComponent<BoxCollider>().size = new Vector3(600f, 600f, 1f);
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

                States.instance.CaveSoulQuest = true;

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
