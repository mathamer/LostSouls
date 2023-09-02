using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LimboSignInteraction : MonoBehaviour
{
    public Text whisperTextPrefab; 
    public string[] sentenceParts; 
    public float displayDuration = 2.0f;
    public float fadeDuration = 1.0f;
    public Canvas uiCanvas; 

    private bool displayOverheadText = false;

    private void OnMouseDown()
    {
        if (!displayOverheadText)
        {
            displayOverheadText = true;

            StartCoroutine(DisplayTextParts());

            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    private IEnumerator DisplayTextParts()
    {
        for (int i = 0; i < sentenceParts.Length; i++)
        {
            Text textComponent = Instantiate(whisperTextPrefab, uiCanvas.transform);
            textComponent.text = sentenceParts[i];

            Vector2 positionOffset = new Vector2(0, 50); 
            textComponent.rectTransform.anchoredPosition = new Vector2(0, positionOffset.y);

            float startTime = Time.time;
            while (Time.time - startTime < fadeDuration)
            {
                float alpha = Mathf.Lerp(0, 1, (Time.time - startTime) / fadeDuration);
                textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, alpha);
                yield return null;
            }

            yield return new WaitForSeconds(displayDuration);

            startTime = Time.time;
            while (Time.time - startTime < fadeDuration)
            {
                float alpha = Mathf.Lerp(1, 0, (Time.time - startTime) / fadeDuration);
                textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, alpha);
                yield return null;
            }

            Destroy(textComponent.gameObject);
        }
    }
}
