using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetSpawnPosition : MonoBehaviour
{
    public Vector3 spawnPositionFromScene2 = new Vector3(-10.86f, 0f, 2.55f); // From scene 2 to Scene 3 
    public Vector3 spawnPositionFromScene4 = new Vector3(12.15f, 0f, 1.04f); // From scene 4 to scene 3  // From village to inside playground

    public Vector3 spawnPositionFromScene3 = new Vector3(430, 0f, 351); // From scene 3 to scene 4
    public Vector3 spawnPositionFromScene5 = new Vector3(612f, 0f, 364f); // From scene 5 to scene 4  // From forrest to village
    public Vector3 spawnPositionFromScene12 = new Vector3(550f, 0f, 351f); // From scene 12 to scene 4  // From church to village
    public Vector3 spawnPositionFromScene4To12 = new Vector3(-4.74f, 0f, 1.55f); // From scene 4 to scene 12 // From village to church

    public Vector3 spawnPositionFromScene10 = new Vector3(589f, 0f, 340f); // From scene 10 to scene 4 // From theater to village //588, 0, 340
    public Vector3 spawnPositionFromScene4To10 = new Vector3(-4.74f, 0f, 1.55f); // From scene 4 to scene 10 // From village to theater
    public Vector3 spawnPositionFromScene9To14 = new Vector3(8.65f, 0f, -45.46f); // From cave to village


    void Start()
    {
        int previousSceneIndex = PlayerPrefs.GetInt("PreviousSceneIndex", -1);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (previousSceneIndex == 2)
            {
                player.transform.position = spawnPositionFromScene2;
            }
            else if (previousSceneIndex == 3)
            {
                player.transform.position = spawnPositionFromScene3;
            }
            else if (previousSceneIndex == 4)
            {
                if (SceneManager.GetActiveScene().buildIndex == 12)
                {
                    // Handle transition from scene 4 to 12
                    player.transform.position = spawnPositionFromScene4To12;
                }
                else if (SceneManager.GetActiveScene().buildIndex == 10)
                {
                    // Handle transition from scene 4 to 10
                    player.transform.position = spawnPositionFromScene4To10;
                }
                else
                {
                    player.transform.position = spawnPositionFromScene4;
                }
            }

            else if (previousSceneIndex == 5)
            {
                player.transform.position = spawnPositionFromScene5;
            }
            else if (previousSceneIndex == 10)
            {
                player.transform.position = spawnPositionFromScene10;
            }
            else if (previousSceneIndex == 12)
            {
                player.transform.position = spawnPositionFromScene12;
            }
            else if (previousSceneIndex == 9)
            {
                player.transform.position = spawnPositionFromScene9To14;
            }

        }

        // Clear the previous scene index after using it
        PlayerPrefs.DeleteKey("PreviousSceneIndex");
    }
}