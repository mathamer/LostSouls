using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EndingDialog : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject panelObject;
    public GameObject dogObject;
    public GameObject lostSoulObject;
    [SerializeField]
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasDisplayedText)
        {
            panelObject.SetActive(true);
            messageText.gameObject.SetActive(true);
            ShowNextSentence();
            hasDisplayedText = true;

            GameObject.Find("Player").GetComponent<RayCast>().DialogStarted();
            gameObject.GetComponent<BoxCollider>().size = new Vector3(600f, 600f, 2f);
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
                panelObject.SetActive(false);
                messageText.gameObject.SetActive(false);

                GameObject.Find("Player").GetComponent<RayCast>().DialogEnded();
                gameObject.GetComponent<BoxCollider>().size = new Vector3(6f, 10f, 16f);

                StartCoroutine(FadeOutObjects());
                States.instance.FirstSoulQuest = true;
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

    IEnumerator FadeOutObjects()
    {
        SpriteRenderer dogRenderer = dogObject.GetComponentInChildren<SpriteRenderer>();
        SpriteRenderer lostSoulRenderer = lostSoulObject.GetComponentInChildren<SpriteRenderer>();

        float fadeDuration = 1.0f;
        float startAlphaDog = dogRenderer.color.a;
        float startAlphaLostSoul = lostSoulRenderer.color.a;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float newAlphaDog = Mathf.Lerp(startAlphaDog, 0f, elapsedTime / fadeDuration);
            float newAlphaLostSoul = Mathf.Lerp(startAlphaLostSoul, 0f, elapsedTime / fadeDuration);

            Color newColorDog = dogRenderer.color;
            newColorDog.a = newAlphaDog;
            dogRenderer.color = newColorDog;

            Color newColorLostSoul = lostSoulRenderer.color;
            newColorLostSoul.a = newAlphaLostSoul;
            lostSoulRenderer.color = newColorLostSoul;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Color finalColorDog = dogRenderer.color;
        finalColorDog.a = 0f;
        dogRenderer.color = finalColorDog;

        Color finalColorLostSoul = lostSoulRenderer.color;
        finalColorLostSoul.a = 0f;
        lostSoulRenderer.color = finalColorLostSoul;

        dogObject.SetActive(false);
        lostSoulObject.SetActive(false);
    }
}
