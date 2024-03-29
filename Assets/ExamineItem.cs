using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamineItem : MonoBehaviour
{
    [Header("Examine Fields")]
    //examine window obj
    public GameObject examineWindow;
    public GameObject mainWindow;
    //refrence image and text
    public Image examineImage;
    public Text examineText;
    public bool isExamining = false;


    void Update()
    {
        float exit = Input.GetAxis("Cancel");
    }
    public void Examine(Item item)
    {
        SpriteRenderer spriteRenderer = item.transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            Debug.Log("Examine");
            examineImage.sprite = spriteRenderer.sprite;
            examineText.text = item.descriptionText;
            mainWindow.SetActive(false);
            examineWindow.SetActive(true);
            isExamining = true;
        }
        else
        {
            Debug.LogError("SpriteRenderer not found on child object of item.");
        }
    }



}
