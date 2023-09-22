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
    private float flickerStartTime;

    public Image monsterImage; 
    public AudioSource soundSource;
    private bool monsterShown = false;

    private void Start()
    {
        blackImage = GetComponent<Image>();
        monsterImage.gameObject.SetActive(false);
        soundSource = GetComponent<AudioSource>(); 
        soundSource.Stop();
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
                flickerStartTime = Time.time;
                PlaySound();
            }
        }
        else if (hasStartedFlickering)
        {
            if (!monsterShown && Time.time - flickerStartTime >= flickerDuration * 0.5f)
            {
                monsterShown = true;
                ShowMonsterImage();
            }

            timer += Time.deltaTime;

            if (timer >= flickerSpeed)
            {
                isFlickering = !isFlickering;
                timer = 0f;
                SetRandomFlickerSpeed();
            }

            blackImage.color = isFlickering ? Color.black : Color.clear;

            if (Time.time - flickerStartTime >= flickerDuration)
            {
                hasStartedFlickering = false;
                blackImage.color = Color.clear; 
                timer = 0f;
            }
        }
    }

    private void SetRandomFlickerSpeed()
    {
        flickerSpeed = Random.Range(minFlickerSpeed, maxFlickerSpeed);
    }

    private float monsterDisplayDuration = 0.5f; 

private void ShowMonsterImage()
{
    monsterImage.gameObject.SetActive(true); 
    Invoke("HideMonsterImage", monsterDisplayDuration);
}

private void HideMonsterImage()
{
    monsterImage.gameObject.SetActive(false); 
}

private void PlaySound()
    {
        soundSource.Play(); // Play the sound
    }

}
