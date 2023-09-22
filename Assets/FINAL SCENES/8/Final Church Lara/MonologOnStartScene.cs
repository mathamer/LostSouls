using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonologOnStartScene : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public GameObject dialogBox;

    [SerializeField]
    private string[] sentences;

    private float textSpeed = 0.1f;
    private int currentSentenceIndex = -1;
    private bool isDisplayingText = false;

    void Start()
    {
        dialogBox.SetActive(false);
        StartDialogScene();
    }

    void StartDialogScene()
    {
        dialogBox.SetActive(true);
        ShowNextSentence();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isDisplayingText)
            {
                // If text is still animating, skip to the end.
                StopAllCoroutines();
                dialogText.text = sentences[currentSentenceIndex];
                isDisplayingText = false;
            }
            else if (currentSentenceIndex < sentences.Length - 1)
            {
                ShowNextSentence();
            }
            else
            {
                // All sentences have been displayed.
                dialogBox.SetActive(false);
            }
        }
    }

    void ShowNextSentence()
    {
        currentSentenceIndex++;
        if (currentSentenceIndex < sentences.Length)
        {
            dialogText.text = "";
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
            dialogText.text += sentence[currentCharacterIndex];
            currentCharacterIndex++;
            yield return new WaitForSeconds(textSpeed);
        }

        isDisplayingText = false;
    }
}
