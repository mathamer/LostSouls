using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindow : MonoBehaviour
{
    [Header("Examine Fields")]
    public GameObject examineWindow;
    public GameObject mainWindow;
    public bool onWindowClosed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            examineWindow.gameObject.SetActive(false);
            mainWindow.gameObject.SetActive(true);
            onWindowClosed = true;
        }
    }

}
