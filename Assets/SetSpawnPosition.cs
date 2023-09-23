using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetSpawnPosition : MonoBehaviour
{
    public Vector3 spawnPositionFromScene3To4 = new Vector3(-10.86f, 0f, 2.55f); // From scene 3 to Scene 4 // from outside playgroung to inside playgroung
    public Vector3 spawnPositionFromScene4To5 = new Vector3(430, 0f, 351); // From scene 4 to scene 5 // from inside playground to village

    public Vector3 spawnPositionFromScene5To4 = new Vector3(12.15f, 0f, 1.04f); // From scene 5 to scene 4  // From village to inside playground

    public Vector3 spawnPositionFromScene6To5 = new Vector3(612f, 0f, 364f); // From scene 6 to scene 5  // From forrest to village
    public Vector3 spawnPositionFromScene5To6 = new Vector3(-13.19f, 0f, 1.2f); // from scene 5 to scene 6 // From village to forest 

    public Vector3 spawnPositionFromScene7To5 = new Vector3(550f, 0f, 351f); // From scene 7 to scene 5  // From church to village
    public Vector3 spawnPositionFromScene5To7 = new Vector3(-4.74f, 0f, 1.55f); // From scene 5 to scene 7 // From village to church

    public Vector3 spawnPositionFromScene8To5 = new Vector3(589f, 0f, 340f); // From scene 8 to scene 5 // From theater to village //588, 0, 340
    public Vector3 spawnPositionFromScene5To8 = new Vector3(-4.74f, 0f, 1.55f); // From scene 5 to scene 8 // From village to theater


    void Start()
{
    int previousSceneIndex = PlayerPrefs.GetInt("PreviousSceneIndex", -1);

    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
    {
        if (previousSceneIndex == 3)
        {
            player.transform.position = spawnPositionFromScene3To4;
        }
        else if (previousSceneIndex == 4)
        {
            player.transform.position = spawnPositionFromScene4To5;
        }
        else if (previousSceneIndex == 5)
        {
            if (SceneManager.GetActiveScene().buildIndex == 7)
            {
                // Handle transition from scene 5 to 7
                player.transform.position = spawnPositionFromScene5To7;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 8)
            {
                // Handle transition from scene 5 to 8
                player.transform.position = spawnPositionFromScene5To8;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 6) 
            {
                // handle position from scene 5 to scene 6
                player.transform.position = spawnPositionFromScene5To6;

            }
            else
            {
                player.transform.position = spawnPositionFromScene5To4;
            }
        }
        
        else if (previousSceneIndex == 6)
        {
            player.transform.position = spawnPositionFromScene6To5;
        }

        else if (previousSceneIndex == 7) 
        {
            player.transform.position = spawnPositionFromScene7To5;
        }

        else if (previousSceneIndex == 8)
        {
            player.transform.position = spawnPositionFromScene8To5;
        }
        
        
    }

    // Clear the previous scene index after using it
    PlayerPrefs.DeleteKey("PreviousSceneIndex");
}
}