using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LostSoulCaveDialog : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject panelObject;
    public float panelTextOffset = 10f;
    public AudioSource textAudio;
    public AudioClip[] clips;

    private string[] sentences = {
    "PLAYER:  HELLO THERE. I'VE NOTICED YOU'RE LOST AND IN NEED OF HELP. WHAT'S TROUBLING YOU?\n",
    "EMILY :  IT'S THE SPIDERS, YOU SEE. I'M SO SCARED I CAN'T GET PAST THEM.\n",
    "PLAYER :  SPIDERS? I UNDERSTAND THAT FEAR CAN BE PARALYZING, BUT DON'T WORRY.\n WE'LL FACE THIS FEAR TOGETHER. CAN YOU TELL ME MORE ABOUT IT?\n",
    "EMILY :  MY BONES, MY MORTAL REMAINS, ARE IN THIS VERY CAVE, AND I REMEMBER NOW.\n I DIED HERE, TANGLED IN WEBS, AND THEY DRAINED MY LIFE AWAY.\n",
    "PLAYER:  I SEE. WE'RE GOING TO FIND YOUR BONES AND ENSURE YOU FIND PEACE.\n",
    "EMILY :  THANK YOU! YOUR WORDS GIVE ME HOPE.\n  I JUST CAN'T BRING MYSELF TO GO BACK THERE.\n",
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
        if (other.CompareTag("Player") && !hasDisplayedText && !States.instance.bonesOnGirl)
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
