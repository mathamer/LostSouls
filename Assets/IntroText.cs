using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class IntroText : MonoBehaviour
{
    public Canvas introCanvas;
    public TextMeshProUGUI introText;
    public float textSpeed = 3f;
    public float lineDelay = 0.1f;

    private bool isIntroDisplayed = false;

    private List<string> introLines = new List<string>
    {
        "I feel disoriented!",
        "Where am I? What happened to me?",
        "The last thing I remember is riding my motorcycle along a winding road!",
        "All I can see is a thick fog surrounding me...",
        "Something feels horribly wrong!",
        "This place exudes an eerie, mystical vibe!",
        "It's like life has abandoned this place long ago.",
        "This looks like a children's playground!",
        "...Is that a cry I hear in the distance?",
        "And what is that at the top? A musical note... covered in BLOOD ?!"
    };

    private void Start()
    {
        Time.timeScale = 0f; 
    }

    private void Update()
    {
        if (!isIntroDisplayed)
        {
            StartCoroutine(DisplayIntroText());
            isIntroDisplayed = true;
        }
    }

    private IEnumerator DisplayIntroText()
{
    introCanvas.gameObject.SetActive(true);

    yield return new WaitForSecondsRealtime(1f); 

    foreach (string line in introLines)
    {
        introText.gameObject.SetActive(true);
        string displayLine = "";
        
        foreach (char c in line)
        {
            displayLine += c;
            introText.text = displayLine;

            yield return new WaitForSecondsRealtime(textSpeed);
        }

        yield return new WaitForSecondsRealtime(lineDelay);

        introText.gameObject.SetActive(false);
    }

    Time.timeScale = 1f;
    introCanvas.gameObject.SetActive(false);
}

}
