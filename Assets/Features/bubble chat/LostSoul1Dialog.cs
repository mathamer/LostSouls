using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LostSoul1Dialog : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject panelObject;
    //public float panelTextOffset = 10f; 
    public Sprite pawPrintSprite;
    private bool isDialogFinished = false;

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
            //ResizePanel();
            messageText.gameObject.SetActive(true);
            ShowNextSentence();
            hasDisplayedText = true;
        }
        if (other.CompareTag("Player") && isDialogFinished)
        {
            SpawnPawPrints();
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
                //ResizePanel();
            }
            else if (currentSentenceIndex < sentences.Length - 1)
            {
                ShowNextSentence();
            }
            else
            {
                isDialogFinished = true;
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
            //ResizePanel();
            yield return new WaitForSeconds(typingSpeed);
        }

        isDisplayingText = false;
        //ResizePanel();
    }

    //void ResizePanel()
    //{
    //    float textWidth = messageText.preferredWidth;
    //    Vector2 panelSize = new Vector2(textWidth + panelTextOffset, panelObject.GetComponent<RectTransform>().sizeDelta.y);
    //    panelObject.GetComponent<RectTransform>().sizeDelta = panelSize;
    //}

    void SpawnPawPrints()
    {
        Vector3 startPoint = new Vector3(533.66f, 0.5478f, 342.94f); // Starting point
        Vector3 endPoint1 = new Vector3(579.4f, 0.5478f, 342.94f); // First bend point
        Vector3 endPoint2 = new Vector3(589.92f, 0.5478f, 362.34f); // Second bend point
        Vector3 endPoint3 = new Vector3(628.93f, 0.5478f, 362.34f); // Ending point
        int numberOfPawPrints = 60; 

        GameObject pawPrintPreview = new GameObject("PawPrintPreview");
        pawPrintPreview.transform.position = new Vector3(533.66f, 0.5478f, 342.94f);
        pawPrintPreview.transform.rotation = Quaternion.Euler(25f, 0f, 270f);
        pawPrintPreview.transform.localScale = new Vector3(0.233f, 0.233f, 1f);
        SpriteRenderer previewSpriteRenderer = pawPrintPreview.AddComponent<SpriteRenderer>();
        previewSpriteRenderer.sprite = pawPrintSprite;

        StartCoroutine(SpawnPawPrintsWithDelay(startPoint, endPoint1, endPoint2, endPoint3, numberOfPawPrints, pawPrintPreview));
    }

    IEnumerator SpawnPawPrintsWithDelay(Vector3 startPoint, Vector3 endPoint1, Vector3 endPoint2, Vector3 endPoint3, int numberOfPawPrints, GameObject pawPrintPreview)
    {
        for (int i = 0; i < numberOfPawPrints; i++)
        {
            float t;
            Vector3 spawnPosition;

            if (i < numberOfPawPrints / 3)
            {
                t = (float)i / (numberOfPawPrints / 3 - 1);
                spawnPosition = Vector3.Lerp(startPoint, endPoint1, t);
            }
            else if (i < (2 * numberOfPawPrints) / 3)
            {
                t = (float)(i - numberOfPawPrints / 3) / (numberOfPawPrints / 3 - 1);
                spawnPosition = Vector3.Lerp(endPoint1, endPoint2, t);
            }
            else
            {
                t = (float)(i - 2 * numberOfPawPrints / 3) / (numberOfPawPrints / 3 - 1);
                spawnPosition = Vector3.Lerp(endPoint2, endPoint3, t);
            }

            RaycastHit hit;
            if (Physics.Raycast(spawnPosition + Vector3.up * 10f, Vector3.down, out hit, Mathf.Infinity))
            {
                spawnPosition.y = hit.point.y; 
            }

            GameObject pawPrintObj = new GameObject("PawPrint");
            pawPrintObj.transform.position = spawnPosition;

            SpriteRenderer spriteRenderer = pawPrintObj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = pawPrintSprite;

            pawPrintObj.transform.rotation = pawPrintPreview.transform.rotation;
            pawPrintObj.transform.localScale = pawPrintPreview.transform.localScale;

            pawPrintObj.transform.position = new Vector3(
                pawPrintObj.transform.position.x,
                pawPrintPreview.transform.position.y,
                pawPrintObj.transform.position.z
            );

            yield return new WaitForSeconds(0.6f); 
        }

        Destroy(pawPrintPreview);
    }


}
