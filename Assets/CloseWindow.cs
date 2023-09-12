using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindow : MonoBehaviour
{
    [Header("Examine Fields")]
    //examine window obj
    public GameObject examineWindow;
    public GameObject mainWindow;
    // public ExamineItem examineItem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            examineWindow.gameObject.SetActive(false);
            mainWindow.gameObject.SetActive(true);
        }
    }

}
