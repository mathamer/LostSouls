using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LostSoul1Dialog : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject panelObject;
    public float panelTextOffset = 10f; 

private string[] sentences = {
    "PLAYER :    HEY THERE!\nI NOTICED YOU PLAYING WITH YOUR BALL.\nMIND IF I JOIN YOU FOR A LITTLE CHAT?\n",
    "LOST SOUL 1 :   DO NOT BOTHER ME!\n",
    "PLAYER :    I CAN SENSE SOMETHING'S BOTHERING YOU.\nIS THERE SOMETHING ON YOUR MIND?\n",
    "LOST SOUL 1 :   *SIGH*\nYEAH, I DON'T KNOW WHAT TO DO.\nI DON'T WANT TO LEAVE MY DOG BEHIND.\nHE'S MY BEST FRIEND, YOU KNOW?\nWE'VE ALWAYS BEEN THERE FOR EACH OTHER.\n",
    "PLAYER :    YOUR DOG MUST MEAN A LOT TO YOU.\nWHY DON'T YOU WANT TO LEAVE HIM?\n",
    "LOST SOUL 1 :   BECAUSE HE'S ALWAYS BEEN THERE\n WHEN NO ONE ELSE WANTED TO PLAY WITH ME.\nHE'S MY ONLY FRIEND, AND I DON'T WANT HIM TO BE ALONE.\n",
    "PLAYER :    I UNDERSTAND.\nIT'S HARD TO SAY GOODBYE TO SOMEONE\n WHO'S BEEN THERE FOR YOU THROUGH THICK AND THIN.\nCAN YOU TELL ME WHAT HAPPENED TO YOUR DOG?\n",
    "LOST SOUL 1 :   WHEN I... WHEN I SUDDENLY LEFT THIS WORLD,\nI TIED MY DOG TO A TREE IN THE WOODS.\nTHE OTHER KIDS FINALLY ASKED ME TO PLAY WITH THEM, AND I WAS SO HAPPY.\n",
    "BUT I COULDN'T UNTIE HIM.\nI DIDN'T WANT TO LEAVE HIM ALONE.\n",
    "PLAYER :    YOUR LOVE FOR YOUR DOG IS TRULY SPECIAL.\nI WANT TO HELP YOU FIND HIM.\nLET'S FOLLOW HIS PAW PRINTS TOGETHER.\nWE'LL BRING HIM BACK, AND YOU WON'T HAVE TO BE APART ANYMORE.\n",
    "LOST SOUL 1 :   REALLY?\nYOU'D DO THAT FOR ME?\nTHANK YOU SO MUCH!\nFIND HIM AND MAKE SURE HE'S SAFE.\nI MISS HIM SO MUCH.\n",
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
