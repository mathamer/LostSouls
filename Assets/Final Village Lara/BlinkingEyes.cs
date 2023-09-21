using System.Collections;
using UnityEngine;

public class BlinkingEyes : MonoBehaviour
{
    public float blinkInterval = 3.0f;
    public float blinkDuration = 0.1f; 
    public float maxAlpha = 1.0f; 
    public float minAlpha = 0.0f; 

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        StartCoroutine(BlinkLoop());
    }

    private IEnumerator BlinkLoop()
    {
        while (true)
        {
            for (float t = 0; t < blinkDuration; t += Time.deltaTime)
            {
                float normalizedTime = t / blinkDuration;
                Color newColor = spriteRenderer.color;
                newColor.a = Mathf.Lerp(maxAlpha, minAlpha, normalizedTime);
                spriteRenderer.color = newColor;
                yield return null;
            }

            for (float t = 0; t < blinkDuration; t += Time.deltaTime)
            {
                float normalizedTime = t / blinkDuration;
                Color newColor = spriteRenderer.color;
                newColor.a = Mathf.Lerp(minAlpha, maxAlpha, normalizedTime);
                spriteRenderer.color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
