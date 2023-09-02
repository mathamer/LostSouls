using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int sceneIndex;

    void OnTriggerEnter(Collider other)
    {
        // Store the previous scene index in PlayerPrefs
        PlayerPrefs.SetInt("PreviousSceneIndex", SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(_ChangeScene());
    }

    public IEnumerator _ChangeScene()
    {
        FadeInOut fade = FindObjectOfType<FadeInOut>();
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
    }
}
