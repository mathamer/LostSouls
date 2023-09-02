using UnityEngine;
using UnityEngine.UI;

public class RiddleZoom : MonoBehaviour
{
    public Canvas zoomCanvas; 
    public float zoomedInScale = 2f; 
    private bool isZoomedIn = false;

    private void Start()
    {
        zoomCanvas.gameObject.SetActive(false); 
    }

    private void OnMouseDown()
    {
        ToggleZoom();
    }

    private void ToggleZoom()
    {
        isZoomedIn = !isZoomedIn;

        zoomCanvas.gameObject.SetActive(isZoomedIn);
        zoomCanvas.transform.localScale = isZoomedIn ? new Vector3(zoomedInScale, zoomedInScale, 1f) : Vector3.one;
    }
}
