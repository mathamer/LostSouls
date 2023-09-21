using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCanvasShake : MonoBehaviour
{
    private RectTransform canvasTransform;
    private Vector3 originalPosition;

    public float shakeDuration = 0.5f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    private void Awake()
    {
        canvasTransform = GetComponent<RectTransform>();
    }

    public void ShakeCanvas()
    {
        originalPosition = canvasTransform.localPosition;
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float duration = shakeDuration;
        while (duration > 0)
        {
            canvasTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;

            duration -= Time.deltaTime * decreaseFactor;
            yield return null;
        }
        canvasTransform.localPosition = originalPosition;
    }
}