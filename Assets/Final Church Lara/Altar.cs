using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Color hoverColor = new Color(1f, 0f, 0f, 1f); // Red color with full alpha (opaque)

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnMouseEnter()
    {
        // Mouse is over the altar
        spriteRenderer.color = hoverColor;
    }

    private void OnMouseExit()
    {
        // Mouse has left the altar
        spriteRenderer.color = originalColor;
    }

    private void OnMouseDown()
    {
        // Handle the click event here (if needed)
    }
}
