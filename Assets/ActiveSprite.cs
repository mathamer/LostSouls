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

        if (hasItem)
        {
            sprite2.SetActive(true);
            sprite1.SetActive(false);

            if (!monsterMusic.isPlaying && closeWindowScript.onWindowClosed)
            {
                monsterMusic.Play();
                bgMusic.Stop();
                AudioSource sprite2Audio = sprite2.GetComponent<AudioSource>();
                if (sprite2Audio != null)
                {
                    sprite2Audio.Play();
                }
            }
        }
        else
        {
            sprite2.SetActive(false);
            sprite1.SetActive(true);

            if (monsterMusic.isPlaying)
            {
                monsterMusic.Stop();
                bgMusic.Play();
            }
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
        }
        return false;
    }

    public void OnMaketaDragged()
    {
        // When Maketa is dragged onto this object, set sprite back to sprite1
        sprite1.SetActive(true);
        sprite2.SetActive(false);
    }

    void OnExamineWindowClosed()
    {
        // Restore original background music when examine window is closed
        if (!monsterMusic.isPlaying)
        {
            monsterMusic.Play();
        }
    }

}
