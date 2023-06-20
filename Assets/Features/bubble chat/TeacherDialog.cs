using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeacherDialog : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject panelObject;
    public float panelTextOffset = 10f; // Offset to add to the text width for panel width

    private string[] sentences = {
    "  TEACHER :  WELCOME, TRAVELER.",
    "YOU FIND YOURSELF IN LIMBO,\nA PLACE BETWEEN LIFE AND DEATH.",
    "I HAVE A STORY TO TELL YOU,\nA TALE OF LOST CHILDREN'S SOULS TRAPPED IN THIS VILLAGE.\nFOR VARIOUS REASONS, THEY REFUSE TO CROSS OVER TO THE AFTERLIFE.",
    "THESE SOULS ARE STUCK IN LIMBO,\nEITHER BECAUSE THEY DENY THEIR OWN DEATH\n OR HAVE SOME OTHER REASON TO STAY.",
    "I WILL BE YOUR GUIDE IN THIS PECULIAR WORLD,\n LEADING YOU THROUGH TASKS YOU MUST COMPLETE TO AID THE LOST SOULS.",
    "ONCE YOU HELP A SOUL,\n THEY WILL JOIN US ON THE PLAYGROUND, AND EVENTUALLY,\n WE WILL ALL CROSS OVER TO THE AFTERLIFE TOGETHER.",
    "I, MYSELF,\n REFUSE TO GO TO THE AFTERLIFE UNTIL\n EVERY LOST SOUL IS WITH ME.",
    "IT IS YOUR MISSION AND RESPONSIBILITY\n TO HELP THESE SOULS FIND THEIR PATH TO PEACE.",
    "IN RETURN,\n I OFFER YOU A WAY HOME,\n BUT ONLY IF YOU FULFILL YOUR MISSION\n AND ASSIST ALL THE LOST SOULS IN CROSSING OVER.",
    "I WILL PROVIDE YOU WITH INSTRUCTIONS\n ON HOW TO PROGRESS THROUGH THE GAME\n AND HELP THE SOULS.",
    "IN RETURN,\n I EXPECT YOU TO GIVE YOUR UTMOST AND AID ME IN COMPLETING MY TASK.",
    "I PROMISE TO GUIDE YOU BACK\n IF YOU SUCCESSFULLY FULFILL YOUR MISSION\n AND HELP ALL THE LOST SOULS FIND THEIR WAY.",
    "  YOU :  : ... ... \nWHERE SHOULD I START? ANY HINTS?",
    "  TEACHER :   IN THE PLACE WHERE A BOY ONCE LIVED WITH HIS BEST FRIEND,\n THE TRACES HAVE BEEN ERASED.\n BUT IF YOU LISTEN CAREFULLY,\n YOU CAN HEAR THE SOUNDS OF THE CHILD'S BALL.\n FOLLOW THE SOUND, THERE YOU WILL FIND THE BOY'S SOUL\n IN SEARCH OF HIS FRIEND."
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
            // Turn off Audio Source of the Teacher
            gameObject.GetComponent<AudioSource>().enabled = false;
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
