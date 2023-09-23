using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Passcode : MonoBehaviour
{
    string Code = "2612";
    string Nr = null;
    int NrIndex = 0;
    string alpha;
    public Text UiText = null;
    public GameObject Keypad;

    public void CodeFunction(string Numbers)
    {
        NrIndex++;
        Nr = Nr + Numbers;
        UiText.text = Nr;
    }

    public void EnterPassword()
    {
        if (Nr == Code)
        {
            UiText.text = "Correct";
            States.instance.correctPasscode = true;
            StartCoroutine(DeactivateKeypadAfterDelay(1f));
        }
        else
        {
            UiText.text = "Invalid";
            StartCoroutine(DeactivateKeypadAfterDelay(1f));
        }
    }

    public void DeleteNr()
    {
        NrIndex++;
        Nr = null;
        UiText.text = Nr;
    }

    private IEnumerator DeactivateKeypadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for 'delay' seconds
        Keypad.SetActive(false); // Deactivate the Keypad
    }
}
