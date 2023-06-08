using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelSizeAdjuster : MonoBehaviour
{
    public RectTransform panelRectTransform;
    public TextMeshProUGUI textMeshPro;

    private void Start()
    {
        ResizePanel();
    }

    private void ResizePanel()
    {
        Vector2 textSize = textMeshPro.GetPreferredValues();
        panelRectTransform.sizeDelta = textSize;
    }
}
