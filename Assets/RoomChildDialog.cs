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

    private string[] sentences = {
    "PLAYER:  HELLO! ARE YOU OKAY?\n",
    "MAX:  PLANE...\n",
    "MAX:  MOTHER...FATHER...\n",
    "MAX:  PLANE....\n",
    "PLAYER:  *I GUESS HE WANTS THIS PLANE MODEL.*\n",
};
    private string[] sentences2 = {
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
        messageText.gameObject.SetActive(false);
        panelObject.SetActive(false);
    }

    void Update()
    {
        if (States.instance.maketaOnMonster)
        {
            sentences = sentences2;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasDisplayedText)
        {
            panelObject.SetActive(true);
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
            }
            else if (currentSentenceIndex < sentences.Length - 1)
            {
                ShowNextSentence();
            }
            else
            {
                panelObject.SetActive(false);
                messageText.gameObject.SetActive(false);

                // TO DOOOOOO OVDJEEEE
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
        isDisplayingText = true;
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        string sentence = sentences[currentSentenceIndex];
        int currentCharacterIndex = 0;
        StartCoroutine(PlayRandomSoundClip());

        while (currentCharacterIndex < sentence.Length)
        {
            messageText.text += sentence[currentCharacterIndex];
            currentCharacterIndex++;
            yield return new WaitForSeconds(typingSpeed);
        }

        isDisplayingText = false;
        textAudio.Stop();
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
