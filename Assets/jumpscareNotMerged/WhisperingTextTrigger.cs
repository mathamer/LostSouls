using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WhisperingTextTrigger : MonoBehaviour
{
    public string[] whisperingTexts;
    public Canvas whisperingCanvas;
    public Text whisperTextPrefab;
    public float displayDuration = 2.0f;
    public float maxHorizontalOffset = 100.0f;
    public float maxVerticalOffset = 50.0f;

    private int currentTextIndex = 0; 
    private bool triggered = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(DisplayPhrases());
        }
    }

    private IEnumerator DisplayPhrases()
    {
        while (currentTextIndex < whisperingTexts.Length)
        {
            string selectedText = whisperingTexts[currentTextIndex];

            Text textComponent = Instantiate(whisperTextPrefab, whisperingCanvas.transform);
            textComponent.text = selectedText;

            Vector3 canvasSize = whisperingCanvas.GetComponent<RectTransform>().sizeDelta;
            Vector3 randomPosition = new Vector3(Random.Range(-maxHorizontalOffset, maxHorizontalOffset), Random.Range(-maxVerticalOffset, maxVerticalOffset), 0);
            textComponent.rectTransform.anchoredPosition = randomPosition;

            currentTextIndex++;

            yield return new WaitForSeconds(displayDuration);

            StartCoroutine(FadeOutText(textComponent));
        }
    }

    private IEnumerator FadeOutText(Text textComponent)
    {
        Color originalColor = textComponent.color;

        for (float t = 0; t < 1.0f; t += Time.deltaTime)
        {
            Color newColor = textComponent.color;
            newColor.a = Mathf.Lerp(originalColor.a, 0, t);
            textComponent.color = newColor;
            yield return null;
        }

        Destroy(textComponent.gameObject);
    }
}
