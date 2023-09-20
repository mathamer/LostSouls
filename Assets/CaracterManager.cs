using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracterManager : MonoBehaviour
{

    public Sprite sprite1;
    public Sprite sprite2;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite1;
    }

    public void ChangeToSprite1()
    {
        spriteRenderer.sprite = sprite1;
    }

    public void ChangeToSprite2()
    {
        spriteRenderer.sprite = sprite2;
    }
}
