using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneToForrest : MonoBehaviour
{
    FadeInOut fade;

    void Start()
    {
        fade = FindObjectOfType<FadeInOut>();

        // fade.FadeOut();
    }

    public IEnumerator _ChangeScene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(5);

    }


    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(_ChangeScene() );
    }
}
