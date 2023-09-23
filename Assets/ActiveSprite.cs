using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSprite : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject objectWithSpriteRenderer;
    public GameObject sprite1;
    public GameObject sprite2;
    public string itemNameToCheck;
    private Vector3 originalScale;
    public AudioSource monsterMusic;
    public AudioSource bgMusic;
    public CloseWindow closeWindowScript;

    void Start()
    {
        originalScale = objectWithSpriteRenderer.transform.localScale;
    }

    void Update()
    {

        bool hasItem = CheckForItem(itemNameToCheck);
        sprite2.SetActive(false);
        sprite1.SetActive(true);

        if (hasItem && closeWindowScript.onWindowClosed)
        {
            sprite2.SetActive(true);
            sprite1.SetActive(false);
            AudioSource sprite2Audio = sprite2.GetComponent<AudioSource>();
            if (sprite2Audio != null)
            {
                Debug.Log("HELOOOOOOO");
                sprite2Audio.Play();
            }

        }

        if (!monsterMusic.isPlaying && closeWindowScript.onWindowClosed && !States.instance.maketaOnMonster)
        {
            monsterMusic.Play();
            bgMusic.Stop();
        }
        else if (monsterMusic.isPlaying && closeWindowScript.onWindowClosed && States.instance.maketaOnMonster)
        {
            monsterMusic.Stop();
            bgMusic.Play();
        }

        if (States.instance.maketaOnMonster)
        {
            sprite2.SetActive(false);
            sprite1.SetActive(true);
        }
    }

    bool CheckForItem(string itemName)
    {
        foreach (InventorySlot slot in inventory.Container)
        {
            if (slot.item != null && slot.item.name == itemName)
            {
                return true;
            }
            else
            {
                monsterMusic.Stop();
                bgMusic.Play();
            }
        }
        return false;
    }


    // public void OnMaketaDragged()
    // {
    //     sprite1.SetActive(true);
    //     sprite2.SetActive(false);
    // }

    void OnExamineWindowClosed()
    {
        // Restore original background music when examine window is closed
        if (!monsterMusic.isPlaying)
        {
            monsterMusic.Play();
        }
    }

}
