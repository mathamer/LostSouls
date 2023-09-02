using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawnPosition : MonoBehaviour
{
    public Vector3 spawnPositionFromScene2 = new Vector3(-10.86f, 0f, 2.55f); // From scene 2 to Scene 3
    public Vector3 spawnPositionFromScene4 = new Vector3(12.15f, 0f, 1.04f); // From scene 4 to scene 3

    public Vector3 spawnPositionFromScene3 = new Vector3(430, 0f, 351); // From scene 3 to scene 4
    public Vector3 spawnPositionFromScene5 = new Vector3(612f, 0f, 364f); // From scene 5 to scene 4

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
            else if (previousSceneIndex == 4)
            {
                player.transform.position = spawnPositionFromScene4;
            }
            else if (previousSceneIndex == 3)
            {
                player.transform.position = spawnPositionFromScene3;
            }
            else if (previousSceneIndex == 5)
            {
                player.transform.position = spawnPositionFromScene5;
            }
        }

        // Clear the previous scene index after using it
        PlayerPrefs.DeleteKey("PreviousSceneIndex");
    }
}
