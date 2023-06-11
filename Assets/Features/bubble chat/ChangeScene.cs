using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    FadeInOut fade;

    public int sceneIndex;

    void Start()
    {
        fade = FindObjectOfType<FadeInOut>();

        // fade.FadeOut();
    }

    public IEnumerator _ChangeScene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);

    }


    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(_ChangeScene());
    }
}
