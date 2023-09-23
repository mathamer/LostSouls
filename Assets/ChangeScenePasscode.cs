using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenePasscode : MonoBehaviour
{
    public int sceneIndex;


    void OnTriggerEnter(Collider other)
    {
        if (States.instance.correctPasscode)
        {
            // Store the previous scene index in PlayerPrefs
            PlayerPrefs.SetInt("PreviousSceneIndex", SceneManager.GetActiveScene().buildIndex);
            StartCoroutine(_ChangeScene());
        }
        else
        {
            Debug.Log("Correct passcode required!");
            FindObjectOfType<PlayerFeedback>().TriggerSentences("hint");
            // You can add code here to provide feedback to the player, like displaying a message.
        }
    }

    public IEnumerator _ChangeScene()
    {
        FadeInOut fade = FindObjectOfType<FadeInOut>();
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
    }
}
