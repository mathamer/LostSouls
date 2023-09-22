using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // to change scenes in Unity

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // dodati u unity scenu od igre, file - build settings - dragg and drop u scenes in build i index mora biti 1  

        // inside States script execute NewGame method
        States.instance.NewGame();
    }

    public void ResumeGame()
    {
        SceneManager.LoadScene(States.instance.currentSceneID);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); //display quit message in console dfgdfgdfgdfg
        Application.Quit();
    }

}
