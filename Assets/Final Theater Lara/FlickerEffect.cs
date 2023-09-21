using UnityEngine;
using UnityEngine.UI;

public class FlickerEffect : MonoBehaviour
{
    public float minFlickerSpeed = 0.4f;
    public float maxFlickerSpeed = 1.0f;

    private Image blackImage;
    private float timer = 0f;
    private bool isFlickering = false;
    private float flickerSpeed;
    private bool hasStartedFlickering = false; 
    private float delayBeforeFlicker = 20f; 
    private float flickerDuration = 10f; 

    private void Start()
    {
        blackImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (!hasStartedFlickering)
        {
            timer += Time.deltaTime;
            if (timer >= delayBeforeFlicker)
            {
                hasStartedFlickering = true;
                SetRandomFlickerSpeed();
            }
        }
        else if (hasStartedFlickering)
        {
            timer += Time.deltaTime;

            if (timer >= flickerSpeed)
            {
                isFlickering = !isFlickering;
                timer = 0f;
                SetRandomFlickerSpeed();
            }

            blackImage.color = isFlickering ? Color.black : Color.clear;

            if (timer >= flickerDuration)
            {
                hasStartedFlickering = false;
                blackImage.color = Color.clear; 
            }
        }
    }

    private void SetRandomFlickerSpeed()
    {
        flickerSpeed = Random.Range(minFlickerSpeed, maxFlickerSpeed);
    }
}
